using System.Runtime.CompilerServices;
using HomeAssignment.Task1.Contracts;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("HomeAssignment.Task1.Services.Tests")]

namespace HomeAssignment.Task1.Services
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
    }
}