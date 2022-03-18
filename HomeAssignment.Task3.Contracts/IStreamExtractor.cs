using System.IO;

namespace HomeAssignment.Task3.Contracts
{
    /// <summary>
    /// Helps to get stream from url
    /// </summary>
    public interface IStreamExtractor
    {
        /// <summary>
        /// Get stream from web file url
        /// </summary>
        /// <param name="fileUrl"></param>
        Stream GetFileStream(string fileUrl);
    }
}