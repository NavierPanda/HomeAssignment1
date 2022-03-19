using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts.DTO;

namespace HomeAssignment.Task4.Contracts
{
    /// <summary>
    /// Markets data source 
    /// </summary>
    public interface IMarketsRepository
    {
        /// <summary>
        /// Get Markets values By assets symbols collection
        /// </summary>
        /// <param name="assetSymbolsCollection"></param>
        Task<IReadOnlyDictionary<string, CollectionMarketType>> GetMarketsByAssetsSymbolCollection(
            IEnumerable<string> assetSymbolsCollection
        );
    }
}