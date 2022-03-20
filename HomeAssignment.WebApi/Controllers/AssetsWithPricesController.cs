using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.WebApi.Controllers
{
    /// <summary>
    /// Assets with prices
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(500)]
    public class AssetsWithPricesController : ControllerBase
    {
        private readonly IAssetsWithPricesService _withPricesService;
        private const int PredefinedLimit = 100;
        private const int BatchSize = 20;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="withPricesService"></param>
        public AssetsWithPricesController(IAssetsWithPricesService withPricesService)
        {
            _withPricesService = withPricesService;
        }

        /// <summary>
        /// Get assets with prices with batch size up to limit
        /// </summary>
        /// <param name="limit">assets limit</param>
        /// <param name="batchSize">batch size for prices extraction</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IReadOnlyCollection<AssetsWithPrices>))]
        public async Task<IActionResult> Get(int limit = PredefinedLimit, int batchSize = BatchSize)
        {
            var assetsWithPricesCollection = await _withPricesService.GetAssetsWithPrices(limit, batchSize);
            return Ok(assetsWithPricesCollection);
        }
    }
}