using System.Runtime.CompilerServices;
using HomeAssignment.Task4.Contracts;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("HomeAssignment.Task4.Services.Tests")]

namespace HomeAssignment.Task4.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add text inversion service
        /// </summary>
        /// <param name="serviceCollection">ms di service collection</param>
        public static IServiceCollection AddDataProviders(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAssetsRepository, AssetsRepository>();
            serviceCollection.AddSingleton<IMarketsRepository, MarketsRepository>();
            serviceCollection.AddSingleton<IAssetsWithPricesService, AssetsWithPricesService>();
            return serviceCollection;
        }

        
    }
}