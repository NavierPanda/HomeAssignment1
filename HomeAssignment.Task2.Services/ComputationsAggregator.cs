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

        public ComputationsAggregator(ILongRunningCalculator longRunningCalculator)
        {
            _longRunningCalculator = longRunningCalculator;
        }

        /// <inheritdoc />
        public async Task<TimeSpan> BuildAggregatedRecord(int numberOfIterations = IterationsConstansts.NumberOfIterations, 
            int msDelay = IterationsConstansts.DelayInMiliseconds)
        {
            TimeSpan taskDelay = new TimeSpan(0, 0, 0, 0, msDelay);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.WhenAll(GetCalculationInput(numberOfIterations).Select(o => _longRunningCalculator.LongRunning(o, taskDelay)).ToArray());
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private IEnumerable<int> GetCalculationInput(int numberOfIterations)
        {
            return Enumerable.Range(0, numberOfIterations);
        }
    }
}