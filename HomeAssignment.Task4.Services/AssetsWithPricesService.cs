using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;

namespace HomeAssignment.Task4.Services
{
    /// <inheritdoc />
    public class AssetsWithPricesService : IAssetsWithPricesService
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMarketsRepository _marketsRepository;

        public AssetsWithPricesService(IAssetsRepository assetsRepository,
            IMarketsRepository marketsRepository)
        {
            _assetsRepository = assetsRepository;
            _marketsRepository = marketsRepository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<AssetsWithPrices>> GetAssetsWithPrices(int limit, int batchSize)
        {
            if (batchSize <= 0)
                throw new ArgumentException("Zero or negative batchSize", nameof(batchSize));
            if (limit <= 0)
                throw new ArgumentException("Zero or negative limit", nameof(batchSize));

            var allAvailableAssets = await _assetsRepository.GetAllAvailableAssets();

            var assetsWithPricesCollection = new List<AssetsWithPrices>();

            var currentlyAdded = 0;

            while (currentlyAdded < limit)
            {
                var requiredToAdd = limit - currentlyAdded;
                var actuallyLeft = allAvailableAssets.Count - currentlyAdded;
                if (actuallyLeft == 0)
                    break;

                var goingToAdd = Math.Min(Math.Min(requiredToAdd, batchSize), actuallyLeft);
                var assetsBatch = allAvailableAssets.Skip(currentlyAdded).Take(
                        goingToAdd)
                    .ToArray();
                var markets = await _marketsRepository
                    .GetMarketsByAssetsSymbolCollection(assetsBatch.Select(o => o.AssetSymbol));

                IEnumerable<AssetsWithPrices> joinedDataQuery =
                    from a in assetsBatch
                    join m in markets on a.AssetSymbol equals m.Key
                    select new AssetsWithPrices
                    {
                        Asset = a,
                        Markets = m.Value.Markets
                    };

                assetsWithPricesCollection.AddRange(joinedDataQuery.ToArray());
                currentlyAdded = assetsWithPricesCollection.Count;
            }


            return assetsWithPricesCollection;
        }
    }
}