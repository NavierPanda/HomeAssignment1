using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using HomeAssignment.Task4.Contracts;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HomeAssignment.Task4.Services.Tests
{
    public class AssetsWithPricesServiceTests
    {
        private MarketsRepository _marketsRepository;

        private IEnumerable<string> _assetsBatch = new[]
        {
            "BTC",
            "ETH",
            "USDT"
        };

        [SetUp]
        public void Setup()
        {
            var someOptions = Options.Create(new BlocktapWebApiOptions
            {
                BaseUrl = "https://api.blocktap.io/graphql"
            });

            _marketsRepository = new MarketsRepository(someOptions);
        }
        
        [Test]
        public void When_WrongUrl_GetAllAvailableAssets_Throws()
        {
            var someOptions = Options.Create(new BlocktapWebApiOptions
            {
                BaseUrl = "https://api.blocktap.io/graphql1111"
            });

            _marketsRepository = new MarketsRepository(someOptions);
            
            var exception = Assert.ThrowsAsync<GraphQLHttpRequestException>( async () => await _marketsRepository
                .GetMarketsByAssetsSymbolCollection(_assetsBatch));
            
        }

        [Test]
        public async Task When_BaseUrlIsRight_GetAllAvailableAssets()
        {
            var marketTypesDictionary = await _marketsRepository
                .GetMarketsByAssetsSymbolCollection(_assetsBatch);
            
            marketTypesDictionary.Should().NotBeNull()
                .And.NotBeEmpty();

            foreach (var assetSymbol in _assetsBatch)
            {
                marketTypesDictionary.Should().ContainKey(assetSymbol);
                marketTypesDictionary[assetSymbol]
                    .Markets.Should().NotBeNull();
            }
        }
    }
}