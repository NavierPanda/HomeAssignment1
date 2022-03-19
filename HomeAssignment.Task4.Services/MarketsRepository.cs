using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Microsoft.Extensions.Options;

namespace HomeAssignment.Task4.Services
{
    /// <inheritdoc />
    internal class MarketsRepository : BaseGraphQlRepository, IMarketsRepository
    {
        private string Query (string someSymbol) => @$"
query price {{
  markets(filter: {{baseSymbol: {{_eq: ""{someSymbol}""}}, quoteSymbol: {{_eq: ""EUR""}}}}) {{
        marketSymbol
            ticker {{
                lastPrice
            }}
    }}
}}";


        public MarketsRepository(IOptions<BlocktapWebApiOptions> options)
            : base(options.Value.BaseUrl)
        {
        }

        /// <inheritdoc />
        public async Task<IReadOnlyDictionary<string, CollectionMarketType>> GetMarketsByAssetsSymbolCollection(
            IEnumerable<string> assetSymbolsCollection)
        {
            var tasksIterator = assetSymbolsCollection
                .Distinct()
                .ToDictionary(o=> o, MarketsByAssetsSymbol);
            await Task.WhenAll(tasksIterator.Values);
            
            return tasksIterator.ToDictionary(o => o.Key, o => o.Value.Result);
        }

        private Task<CollectionMarketType> MarketsByAssetsSymbol(string singleAssetSymbol)
        {
            var getAllAvailableAssetsRequest = new GraphQLRequest
            {
                Query = Query(singleAssetSymbol)
            };
            
            return GetResultsFromQuery<CollectionMarketType>(getAllAvailableAssetsRequest);
        }
    }
}