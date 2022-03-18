using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignment.Task2.Contracts;

namespace HomeAssignment.Task2.Services
{
    /// <inheritdoc />
    public class ComputationsAggregator : IComputationsAggregator
    {
        private readonly ILongRunningCalculator _longRunningCalculator;
        private const int NumberOfIterations = 1000;

        public ComputationsAggregator(ILongRunningCalculator longRunningCalculator)
        {
            _longRunningCalculator = longRunningCalculator;
        }

        /// <inheritdoc />
        public async Task<TimeSpan> BuildAggregatedRecord()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.WhenAll(GetCalculationInput().Select(o => _longRunningCalculator.LongRunning(o)).ToArray());
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private IEnumerable<int> GetCalculationInput()
        {
            return Enumerable.Range(0, NumberOfIterations);
        }
    }
}