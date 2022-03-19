using HomeAssignment.Task1.Contracts;
using HomeAssignment.WebApi.Contracts;
using HomeAssignment.WebApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(500)]
    public class InvertTextController : ControllerBase
    {
        private readonly StaticStringResourcesOptions _options;
        private readonly ITextInverterService _textInverterService;

        public InvertTextController(
            IOptions<StaticStringResourcesOptions> options, 
            ITextInverterService textInverterService)
        {
            _options = options.Value;
            _textInverterService = textInverterService;
        }

        [HttpGet]
        // here we can enlist all error codes
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(ReverseAlgorithmEnum howToDo, string originalString = null)
        {
            string input = originalString ?? _options.Lorem;
            string invertString;
            switch (howToDo)
            {
                case ReverseAlgorithmEnum.ReverseChars:
                    invertString = _textInverterService.InvertStringChars(input);
                    break;
                case ReverseAlgorithmEnum.ReverseWordOrder:
                    invertString = _textInverterService.ReverseWordOrder(input);
                    break;
                default:
                    invertString = string.Empty;
                    break;
            }
            
            return Ok(invertString);
        }
    }
}