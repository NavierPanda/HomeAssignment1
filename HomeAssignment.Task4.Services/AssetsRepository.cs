using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Microsoft.Extensions.Options;

namespace HomeAssignment.Task4.Services
{
    /// <inheritdoc />
    internal sealed class AssetsRepository : BaseGraphQlRepository, IAssetsRepository
    {
        private const string Query = @"
query PageAssets {
  assets(sort: [{marketCapRank: ASC}]) {
    assetName
    assetSymbol
    marketCap
  }
}";

        public AssetsRepository(IOptions<BlocktapWebApiOptions> options)
            : base(options.Value.BaseUrl)
        {
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<AssetsType>> GetAllAvailableAssets()
        {
            var getAllAvailableAssetsRequest = new GraphQLRequest {Query = Query};
            return (await GetResultsFromQuery<CollectionAssetsType>(getAllAvailableAssetsRequest))
                   ?.Assets ?? new List<AssetsType>();
        }
    }
}