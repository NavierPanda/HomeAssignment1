using System.IO;

namespace HomeAssignment.Task3.Contracts
{
    /// <summary>
    /// Calculate Calculate a SHA hash (in hex form)
    /// </summary>
    public interface ISHACalculator
    {
        /// <summary>
        /// Calculate a SHA hash from stream
        /// </summary>
        /// <param name="resStream">valid stream</param>
        string Calc(Stream resStream);
    }
}