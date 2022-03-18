using System.Runtime.CompilerServices;
using HomeAssignment.Contracts;
using HomeAssignment.Task3.Contracts;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("HomeAssignment.Task3.Services.Tests")]


namespace HomeAssignment.Task3.Services
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add Hash Calculation Services
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddHashCalculation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUrlValidator, UrlValidator>();
            serviceCollection.AddSingleton<IStreamExtractor, StreamExtractor>();
            serviceCollection.AddSingleton<ISHACalculator, SHACalculator>();
            
            serviceCollection.AddSingleton<ISHACalcService>(
                s =>
                {
                    var streamExtractor = s.GetRequiredService<IStreamExtractor>();
                    var urlValidator = s.GetRequiredService<IUrlValidator>();
                    var shaCalculator = s.GetRequiredService<ISHACalculator>();

                    return new SHACalcServiceDecorator(
                        new SHACalcService(urlValidator, streamExtractor, shaCalculator)
                    );
                }
            );
            
            return serviceCollection;
        }
    }
}