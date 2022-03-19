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

        /// <inheritdoc />
        public string ReverseWordOrder(string dataToInvert)
        {
            if (String.IsNullOrEmpty(dataToInvert))
                return dataToInvert;
            
            var charArray = dataToInvert.ToCharArray();
            int start = 0;
            for (int end = 0; end < charArray.Length; end++)
            {
                // If we see a space, we reverse 
                // the previous word (word between 
                // the indexes start and end - 1 
                // i.e., s[start..end-1]
                if (charArray[end] == ' ')
                    //|| charArray[end] == ',' || charArray[end] == '.')
                {
                    Array.Reverse(charArray, start, end-start);
                    start = end + 1;
                }
            }

            // Reverse the last word
            Array.Reverse(charArray, start, charArray.Length - start);

            // Reverse the entire String
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
        
    }
}