using System;
using System.Threading.Tasks;
using HomeAssignment.Task2.Contracts;

namespace HomeAssignment.Task2.Services
{
    /// <inheritdoc />
    public class LongRunningCalculator : ILongRunningCalculator
    {
        private const int TaskDelay = 10;

        /// <inheritdoc />
        public async Task<bool> LongRunning(int inputNumberParam)
        {
            await Task.Delay(new TimeSpan(0, 0, TaskDelay));
            return true;
        }
    }
}