using System;
using System.Collections.Concurrent;
using HomeAssignment.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <summary>
    /// SHACalculator cache
    /// </summary>
    internal sealed class SHACalculatorDecorator : ISHACalculator
    {
        private readonly ISHACalculator _decorated;
        private readonly IUrlValidator _urlValidator;
        
        private readonly ConcurrentDictionary<string, string> _hashesCache = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, object> _calculationLocks =
            new ConcurrentDictionary<string, object>();
        
        public SHACalculatorDecorator(ISHACalculator decorated, IUrlValidator urlValidator)
        {
            _decorated = decorated;
            _urlValidator = urlValidator;
        }


        /// <inheritdoc />
        public string Calc(string fileUrl)
        {
            if (!_urlValidator.IsValidFileUrl(fileUrl))
                throw new ArgumentException("Invalid file url passed");
            
            lock (_calculationLocks.GetOrAdd(fileUrl, f => new object()))
            {
                return _hashesCache.GetOrAdd(fileUrl, f => _decorated.Calc(fileUrl));
            }
        }
    }
}