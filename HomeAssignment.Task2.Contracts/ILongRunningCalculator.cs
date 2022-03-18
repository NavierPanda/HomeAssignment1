using System.Threading.Tasks;

namespace HomeAssignment.Task2.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILongRunningCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputNumberParam"></param>
        Task<bool> LongRunning(int inputNumberParam);
    }
}