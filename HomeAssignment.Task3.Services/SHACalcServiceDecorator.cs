using System;
using System.Collections.Concurrent;
using HomeAssignment.Task3.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <summary>
    /// SHACalculator cache
    /// </summary>
    internal sealed class SHACalcServiceDecorator : ISHACalcService
    {
        private readonly ISHACalcService _decorated;

        private readonly ConcurrentDictionary<string, string> _hashesCache = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, object> _calculationLocks =
            new ConcurrentDictionary<string, object>();
        
        public SHACalcServiceDecorator(ISHACalcService decorated)
        {
            _decorated = decorated;
        }


        /// <inheritdoc />
        public string Calc(string fileUrl)
        {
            lock (_calculationLocks.GetOrAdd(fileUrl, f => new object()))
            {
                return _hashesCache.GetOrAdd(fileUrl, f => _decorated.Calc(fileUrl));
            }
        }
    }
}