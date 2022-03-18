using System.Runtime.CompilerServices;
using HomeAssignment.Task2.Contracts;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("HomeAssignment.Task2.Services.Tests")]

namespace HomeAssignment.Task2.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Parallel Computations
        /// </summary>
        /// <param name="serviceCollection">ms di service collection</param>
        public static IServiceCollection AddLongRunningComputations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILongRunningCalculator, LongRunningCalculator>();
            serviceCollection.AddSingleton<IComputationsAggregator, ComputationsAggregator>();
            return serviceCollection;
        }

        
    }
}