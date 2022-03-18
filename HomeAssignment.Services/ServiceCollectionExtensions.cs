using System.Runtime.CompilerServices;
using HomeAssignment.Contracts;
using HomeAssignment.Services.Task1;
using HomeAssignment.Services.Task3;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("HomeAssignment.Services.Tests")]

namespace HomeAssignment.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add text inversion service
        /// </summary>
        /// <param name="serviceCollection">ms di service collection</param>
        public static IServiceCollection AddTextInversion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITextInverterService, TextInverterService>();
            return serviceCollection;
        }

        /// <summary>
        /// Add Hash Calculation Services
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddHashCalculation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUrlValidator, UrlValidator>();
            
            serviceCollection.AddSingleton<ISHACalculator>(
                s =>
                {
                    var validator = s.GetRequiredService<IUrlValidator>();

                    return new SHACalculatorDecorator(
                        new SHACalculator(validator),
                        validator
                    );
                }
            );
            
            return serviceCollection;
        }
    }
}