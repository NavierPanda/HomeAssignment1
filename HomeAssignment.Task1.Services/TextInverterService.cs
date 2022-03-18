using System;
using HomeAssignment.Task1.Contracts;

namespace HomeAssignment.Task1.Services
{
    /// <inheritdoc />
    internal class TextInverterService : ITextInverterService
    {
        /// <inheritdoc />
        public string InvertStringChars(string dataToInvert)
        {
            if (String.IsNullOrEmpty(dataToInvert))
                return dataToInvert;
            
            var charArray = dataToInvert.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}