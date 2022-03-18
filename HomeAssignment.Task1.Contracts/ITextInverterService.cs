namespace HomeAssignment.Task1.Contracts
{
    /// <summary>
    /// Service that helps to invert data in a string
    /// </summary>
    public interface ITextInverterService
    {
        /// <summary>
        /// Invert data in a string
        /// </summary>
        /// <param name="dataToInvert">input</param>
        /// <returns>inverted string</returns>
        string InvertStringChars(string dataToInvert);
    }
}