using System.IO;
using System.Net;
using HomeAssignment.Task3.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <inheritdoc />
    internal sealed class StreamExtractor : IStreamExtractor
    {
        /// <inheritdoc />
        public Stream GetFileStream(string fileUrl)
        {
            var req = WebRequest.Create(fileUrl);
            return req.GetResponse().GetResponseStream();
        }
    }
}