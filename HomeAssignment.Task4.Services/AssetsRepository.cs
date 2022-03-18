using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Microsoft.Extensions.Options;

namespace HomeAssignment.Task4.Services
{
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

        public Task<CollectionAssetsType> GetAllAvailableAssets()
        {
            var getAllAvailableAssetsRequest = new GraphQLRequest {Query = Query};
            return GetResultsFromQuery<CollectionAssetsType>(getAllAvailableAssetsRequest);
        }
    }
}