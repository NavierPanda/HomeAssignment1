using System;
using System.Collections.Generic;
using HomeAssignment.Task3.Contracts;
using HomeAssignment.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(500)]
    public class SHACalcController : ControllerBase
    {
        private readonly ISHACalcService _calcService;

        private readonly IDictionary<PredefinedFileUrlEnum, string> _fileUrlsDictionary = new
            Dictionary<PredefinedFileUrlEnum, string>
            {
                {PredefinedFileUrlEnum.File100Mb, "https://speed.hetzner.de/100MB.bin"},
                {PredefinedFileUrlEnum.File1Gb, "https://speed.hetzner.de/1GB.bin"},
                {PredefinedFileUrlEnum.File10Gb, "https://speed.hetzner.de/10GB.bin"}
            };

        public SHACalcController(ISHACalcService calcService)
        {
            _calcService = calcService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        public ActionResult Get(string fileUrl = null)
        {
            if (String.IsNullOrEmpty(fileUrl))
                return Ok(String.Empty);
            
            var hashResult = _calcService.Calc(fileUrl);
            return Ok(hashResult);
        }

        [HttpGet]
        [Route("predefined")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(PredefinedFileUrlEnum fileUrlEnum)
        {
            if (!_fileUrlsDictionary.ContainsKey(fileUrlEnum))
                return Ok(String.Empty);

            var hashResult = _calcService.Calc(_fileUrlsDictionary[fileUrlEnum]);
            return Ok(hashResult);
        }
    }
}