using System.Runtime.CompilerServices;
using HomeAssignment.Contracts;
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