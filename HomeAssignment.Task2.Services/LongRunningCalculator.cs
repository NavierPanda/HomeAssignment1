using System;
using System.Threading.Tasks;
using HomeAssignment.Task2.Contracts;

namespace HomeAssignment.Task2.Services
{
    /// <inheritdoc />
    public class LongRunningCalculator : ILongRunningCalculator
    {
        private static readonly TimeSpan TaskDelay = new TimeSpan(0, 0, 0, 0, 1);

        /// <inheritdoc />
        public async Task<bool> LongRunning(int inputNumberParam)
        {
            await Task.Delay(TaskDelay);
            return true;
        }
    }
}