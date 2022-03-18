using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts.DTO;

namespace HomeAssignment.Task4.Contracts
{
    /// <summary>
    /// Assets with prices service
    /// </summary>
    public interface IAssetsWithPricesService
    {
        /// <summary>
        /// Get part of assets with prices
        /// </summary>
        /// <param name="limit">limits results cap</param>
        /// <param name="batchSize">batch size for price loading</param>
        Task<IReadOnlyCollection<AssetsWithPrices>> GetAssetsWithPrices(int limit, int batchSize);
    }
}