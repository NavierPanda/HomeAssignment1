using System;
using HomeAssignment.Contracts;
using HomeAssignment.Task3.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <inheritdoc />
    public class SHACalcService : ISHACalcService
    {
        //https://speed.hetzner.de/100MB.bin
        //https://speed.hetzner.de/1GB.bin
        //https://speed.hetzner.de/10GB.bin
        
        public const string  InvalidFileUrlPassed = "Invalid file url passed";
        
        private readonly IUrlValidator _urlValidator;
        private readonly IStreamExtractor _streamExtractor;
        private readonly ISHACalculator _shaCalculator;

        public SHACalcService(
            IUrlValidator urlValidator,
            IStreamExtractor streamExtractor,
            ISHACalculator shaCalculator)
        {
            _urlValidator = urlValidator;
            _streamExtractor = streamExtractor;
            _shaCalculator = shaCalculator;
        }


        /// <inheritdoc />
        public string Calc(string fileUrl)
        {
            if (!_urlValidator.IsValidFileUrl(fileUrl))
            {
                throw new ArgumentException(InvalidFileUrlPassed);
            }
            using var resStream = _streamExtractor.GetFileStream(fileUrl);
            return _shaCalculator.Calc(resStream);
        }
    }
}