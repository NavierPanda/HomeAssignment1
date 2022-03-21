using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Moq;
using NUnit.Framework;

namespace HomeAssignment.Task4.Services.Tests
{
    public class AssetsWithPricesServiceTests
    {
        private Mock<IAssetsRepository> _assetsRepositoryMock;

        private IEnumerable<AssetsType> _assetsBatch = new AssetsType[]
        {
            new AssetsType
            {
                AssetName = "Bitcoin",
                AssetSymbol = "BTC",
                MarketCap = 793087584578
            },
            new AssetsType
            {
                AssetName = "Ethereum",
                AssetSymbol = "ETH",
                MarketCap = 354266866531
            },
            new AssetsType
            {
                AssetName = "USD Tether",
                AssetSymbol = "USDT",
                MarketCap = 80456921164
            },
            new AssetsType
            {
                AssetName = "Binance Coin",
                AssetSymbol = "BNB",
                MarketCap = 65892522203
            },
            new AssetsType
            {
                AssetName =  "USD Coin",
                AssetSymbol = "USDC",
                MarketCap = 52854825624
            },
            new AssetsType
            {
                AssetName = "Ripple",
                AssetSymbol = "XRP",
                MarketCap = 38305952541
            }
        };

        [SetUp]
        public void Setup()
        {
            _assetsRepositoryMock = new Mock<IAssetsRepository>();
            _assetsRepositoryMock.Setup(o => o.GetAllAvailableAssets())
                .ReturnsAsync(new List<AssetsType>());
        }

        [Test]
        public async Task When_NoAllAvailableAssets_ReturnsEmpty()
        {
            var marketsRepositoryMock = new Mock<IMarketsRepository>();
            
            var assetsWithPricesService = new AssetsWithPricesService(
                _assetsRepositoryMock.Object,
                marketsRepositoryMock.Object
            );
            
            var dataRecords = await assetsWithPricesService
                .GetAssetsWithPrices(100, 20);
            dataRecords.Should().NotBeNull()
                .And.BeEmpty();
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -1)]
        [TestCase(-10, -1)]
        public void When_InvalidParams_Throws(int limit, int batchSize)
        {
            var marketsRepositoryMock = new Mock<IMarketsRepository>();
            
            var assetsWithPricesService = new AssetsWithPricesService(
                _assetsRepositoryMock.Object,
                marketsRepositoryMock.Object
            );
            
            var exception = Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsWithPricesService.GetAssetsWithPrices(limit, batchSize)
            );
            exception.Should().NotBeNull();
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task When_NotEnoughData_ReturnsLessThanLimit(int batchSize)
        {
            var assetsAvailable = 3;
            var limit = 4;

            var assetsList = _assetsBatch.Take(assetsAvailable).ToList();
            
            _assetsRepositoryMock.Setup(o => o.GetAllAvailableAssets())
                .ReturnsAsync(() => assetsList);

            var preparedMock = new MarketsRepositoryMockBuilder()
                .WithFilledMarketsForSymbol(assetsList[0].AssetSymbol)
                .WithEmptyMarketsForSymbol(assetsList[1].AssetSymbol)
                .WithFilledMarketsForSymbol(assetsList[2].AssetSymbol)
                .BuildMock();

            var assetsWithPricesService = new AssetsWithPricesService(
                _assetsRepositoryMock.Object,
                preparedMock.Object
            );
            
            var assetsWithPrices = await assetsWithPricesService
                .GetAssetsWithPrices(limit, batchSize);

            assetsWithPrices.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(assetsAvailable);

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task When_EnoughData_ReturnsNoMoreThanLimit(int batchSize)
        {
            var assetsAvailable = 5;
            var limit = 4;

            var assetsList = _assetsBatch.Take(assetsAvailable).ToList();
            
            _assetsRepositoryMock.Setup(o => o.GetAllAvailableAssets())
                .ReturnsAsync(() => assetsList);

            var preparedMock = new MarketsRepositoryMockBuilder()
                .WithFilledMarketsForSymbol(assetsList[0].AssetSymbol)
                .WithEmptyMarketsForSymbol(assetsList[1].AssetSymbol)
                .WithFilledMarketsForSymbol(assetsList[2].AssetSymbol)
                .WithFilledMarketsForSymbol(assetsList[3].AssetSymbol)
                .WithEmptyMarketsForSymbol(assetsList[4].AssetSymbol)
                .BuildMock();

            var assetsWithPricesService = new AssetsWithPricesService(
                _assetsRepositoryMock.Object,
                preparedMock.Object
            );
            
            var assetsWithPrices = await assetsWithPricesService
                .GetAssetsWithPrices(limit, batchSize);

            assetsWithPrices.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(limit);

        }
    }
}