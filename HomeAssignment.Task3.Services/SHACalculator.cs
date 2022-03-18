using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using HomeAssignment.Contracts;

namespace HomeAssignment.Task3.Services
{
    /// <inheritdoc />
    internal sealed class SHACalculator : ISHACalculator
    {
        public const string  InvalidFileUrlPassed = "Invalid file url passed";
        private readonly IUrlValidator _urlValidator;

        public SHACalculator(IUrlValidator urlValidator)
        {
            _urlValidator = urlValidator;
        }

        public string Calc(string fileUrl)
        {
            if (!_urlValidator.IsValidFileUrl(fileUrl))
            {
                throw new ArgumentException(InvalidFileUrlPassed);
            }

            var resStream = GetFileStream(fileUrl);
            if (resStream == null)
                return String.Empty;
            
            var resultedHash = new SHA1CryptoServiceProvider().ComputeHash(resStream);
            return BitConverter.ToString(resultedHash);
        }

        private static Stream GetFileStream(string fileUrl)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(fileUrl);
            // execute the request
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            // we will read data via the response stream
            var resStream = response.GetResponseStream();
            return resStream;
        }
    }
}