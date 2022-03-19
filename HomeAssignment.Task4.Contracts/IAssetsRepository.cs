using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts.DTO;

namespace HomeAssignment.Task4.Contracts
{
    /// <summary>
    /// Assets data source
    /// </summary>
    public interface IAssetsRepository
    {
        /// <summary>
        /// Get all available assets from external data source
        /// </summary>
        Task<IReadOnlyCollection<AssetsType>> GetAllAvailableAssets();
    }
}