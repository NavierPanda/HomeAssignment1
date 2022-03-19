using System;
using System.Threading.Tasks;
using HomeAssignment.Task2.Contracts;

namespace HomeAssignment.Task2.Services
{
    /// <inheritdoc />
    public class LongRunningCalculator : ILongRunningCalculator
    {
        /// <inheritdoc />
        public async Task<bool> LongRunning(int inputNumberParam, TimeSpan timeSpanDelay)
        {
            await Task.Delay(timeSpanDelay);
            return true;
        }
    }
}