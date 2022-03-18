using System;
using System.IO;
using System.Security.Cryptography;
using HomeAssignment.Task3.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <inheritdoc />
    internal sealed class SHACalculator : ISHACalculator
    {
        public const string UnreadableStream = "Unreadable stream";

        /// <inheritdoc />
        public string Calc(Stream resStream)
        {
            if (resStream == null || !resStream.CanRead)
            {
                throw new ArgumentException(UnreadableStream, nameof(resStream));
            }

            using var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            var resultedHash = sha1CryptoServiceProvider.ComputeHash(resStream);
            return BitConverter.ToString(resultedHash);
        }
    }
}