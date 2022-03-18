using System.Threading.Tasks;

namespace HomeAssignment.Task2.Contracts
{
    /// <summary>
    /// Some long running calculator
    /// </summary>
    public interface ILongRunningCalculator
    {
        /// <summary>
        /// Some long running method
        /// </summary>
        /// <param name="inputNumberParam">input parametr</param>
        Task<bool> LongRunning(int inputNumberParam);
    }
}