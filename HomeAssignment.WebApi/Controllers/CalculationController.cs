using System;
using System.Threading.Tasks;
using HomeAssignment.Task2.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(500)]
    public class CalculationController : ControllerBase
    {
        private readonly IComputationsAggregator _computationsAggregator;

        public CalculationController(IComputationsAggregator computationsAggregator)
        {
            _computationsAggregator = computationsAggregator;
        }

        [HttpPost]
        // here we can enlist all error codes
        [ProducesResponseType(200, Type = typeof(TimeSpan))]
        public async Task<IActionResult> Calc(int? numberOfIterations = null, int? msDelay = null)
        {
            TimeSpan result;
            if (numberOfIterations != null && msDelay != null)
            {
                result = await _computationsAggregator.BuildAggregatedRecord(numberOfIterations.Value, msDelay.Value);
            }
            else if (numberOfIterations != null)
            {
                result = await _computationsAggregator.BuildAggregatedRecord(numberOfIterations.Value);
            }
            else if (msDelay != null)
            {
                result = await _computationsAggregator.BuildAggregatedRecord(msDelay: msDelay.Value);
            }
            else
            {
                result = await _computationsAggregator.BuildAggregatedRecord();
            }

            return Ok(result);
        }
    }
}