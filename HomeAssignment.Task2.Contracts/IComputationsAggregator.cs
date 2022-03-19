using System;
using System.Threading.Tasks;

namespace HomeAssignment.Task2.Contracts
{
    /// <summary>
    /// Computations Results Aggregator
    /// </summary>
    public interface IComputationsAggregator
    {
        /// <summary>
        /// Calculate and return evaluation time
        /// </summary>
        Task<TimeSpan> BuildAggregatedRecord(int numberOfIterations = IterationsConstansts.NumberOfIterations,
            int msDelay = IterationsConstansts.DelayInMiliseconds);
    }
}