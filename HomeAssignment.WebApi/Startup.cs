using System;
using System.IO;
using System.Reflection;
using HomeAssignment.Task1.Services;
using HomeAssignment.Task2.Services;
using HomeAssignment.Task3.Services;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Services;
using HomeAssignment.WebApi.Middlewares;
using HomeAssignment.WebApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

#pragma warning disable 1591
namespace HomeAssignment.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options=> options.SerializerSettings.Converters.Add(new StringEnumConverter()));;

            services.AddOptions();

            services.Configure<StaticStringResourcesOptions>(
                Configuration.GetSection(StaticStringResourcesOptions.ConfigSectionKey));
            services.Configure<BlocktapWebApiOptions>(
                Configuration.GetSection(BlocktapWebApiOptions.ConfigSectionKey));
            
            services.AddSwaggerGen(
                config =>
                {
                    config.IncludeXmlComments(XmlCommentsFilePath);

                    config.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Home Assignment", Version = "v1"
                    });
                }
            );
            services.AddSwaggerGenNewtonsoftSupport();

            AddBusinessServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(
                config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", 
                    "Home Assignment Api"); }
            );

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddBusinessServices(IServiceCollection services)
        {
            services.AddTextInversion();
            services.AddLongRunningComputations();
            services.AddHashCalculation();
            services.AddDataProviders();
        }
        
        static string XmlCommentsFilePath
        {
            get
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                return Path.Combine(AppContext.BaseDirectory, xmlFilename);
            }
        }
    }
}
#pragma warning restore 1591