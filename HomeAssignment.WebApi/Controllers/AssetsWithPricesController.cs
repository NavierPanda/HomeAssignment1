using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignment.Task4.Contracts;
using HomeAssignment.Task4.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetsWithPricesController: ControllerBase
    {
        private readonly IAssetsWithPricesService _withPricesService;
        private const int PredefinedLimit = 100;
        private const int BatchSize = 20;

        public AssetsWithPricesController(IAssetsWithPricesService withPricesService)
        {
            _withPricesService = withPricesService;
        }

        [HttpGet]
        // here we can enlist all error codes
        [ProducesResponseType(200, Type = typeof(IReadOnlyCollection<AssetsWithPrices>))]
        public async Task<IActionResult> Get(int limit = PredefinedLimit, int batchSize = BatchSize)
        {
            try
            {
                var assetsWithPricesCollection = await _withPricesService.GetAssetsWithPrices(limit, batchSize);
                return Ok(assetsWithPricesCollection);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.ParamName);
            }
        }
    }
}