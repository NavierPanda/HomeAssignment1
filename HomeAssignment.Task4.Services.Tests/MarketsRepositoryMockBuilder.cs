using System;
using System.Collections.Generic;
using System.Linq;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Moq;

namespace HomeAssignment.Task4.Services.Tests
{
    /// <summary>
    /// Builder to help mocking IMarketsRepository
    /// </summary>
    internal class MarketsRepositoryMockBuilder
    {
        private readonly Mock<IMarketsRepository> _marketsRepositoryMock;
        private readonly IDictionary<string, List<MarketType>> _dictionaryMarketTypes;

        public MarketsRepositoryMockBuilder()
        {
            _marketsRepositoryMock = new Mock<IMarketsRepository>();
            _dictionaryMarketTypes = new Dictionary<string, List<MarketType>>();
        }

        /// <summary>
        /// Setup assetSymbol to return empty markets data
        /// </summary>
        public MarketsRepositoryMockBuilder WithEmptyMarketsForSymbol(string assetSymbol)
        {
            if (!_dictionaryMarketTypes.ContainsKey(assetSymbol))
                _dictionaryMarketTypes.Add(assetSymbol, Empty());
            else
                _dictionaryMarketTypes[assetSymbol] = Empty();
            return this;
        }

        /// <summary>
        /// Setup assetSymbol to return not empty filled markets data
        /// </summary>
        public MarketsRepositoryMockBuilder WithFilledMarketsForSymbol(string assetSymbol)
        {
            if (!_dictionaryMarketTypes.ContainsKey(assetSymbol))
                _dictionaryMarketTypes.Add(assetSymbol, SomeMarketTypes(assetSymbol));
            else
                _dictionaryMarketTypes[assetSymbol] = SomeMarketTypes(assetSymbol);
            return this;
        }

        /// <summary>
        /// Build mock with saved setups
        /// </summary>
        public Mock<IMarketsRepository> BuildMock()
        {
            _marketsRepositoryMock.Setup(r => r.GetMarketsByAssetsSymbolCollection(
                    It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((IEnumerable<string> strs) =>
                {
                    return strs.Distinct()
                        .ToDictionary<string, string, CollectionMarketType>(s => s,
                            s =>
                                new CollectionMarketType
                                {
                                    Markets = _dictionaryMarketTypes.ContainsKey(s)
                                        ? _dictionaryMarketTypes[s]
                                        : Empty()
                                }
                        );
                });
            return _marketsRepositoryMock;
        }

        private static List<MarketType> Empty()
        {
            return Array.Empty<MarketType>().ToList();
        }

        private static List<MarketType> SomeMarketTypes(string assetSymbol)
        {
            return new List<MarketType>
            {
                new MarketType
                {
                    MarketSymbol = $"Binance:{assetSymbol}/EUR",
                    Ticker = new TickerType
                    {
                        LastPrice = 42515.34000000m
                    }
                },
                new MarketType
                {
                    MarketSymbol = $"BinanceJe:{assetSymbol}/EUR",
                    Ticker = null
                },
                new MarketType
                {
                    MarketSymbol = $"Bitfinex:{assetSymbol}/EUR",
                    Ticker = new TickerType
                    {
                        LastPrice = 2620.90000000m
                    }
                }
            };
        }
    }
}