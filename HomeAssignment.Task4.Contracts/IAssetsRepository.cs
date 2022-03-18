using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts.DTO;

namespace HomeAssignment.Task4.Contracts
{
    public interface IAssetsRepository
    {
        Task<CollectionAssetsType> GetAllAvailableAssets();
    }
}