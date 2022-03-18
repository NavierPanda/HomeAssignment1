using System;
using HomeAssignment.Contracts;

namespace HomeAssignment.Task3.Services
{
    internal sealed class UrlValidator : IUrlValidator
    {
        public bool IsValidFileUrl(string fileUrl)
        {
            return Uri.TryCreate(fileUrl, UriKind.Absolute, out var uriResult) 
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}