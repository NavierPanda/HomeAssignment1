namespace HomeAssignment.WebApi.Contracts
{
    /// <summary>
    /// Supported text inversion algorithms
    /// </summary>
    public enum ReverseAlgorithmEnum
    {
        /// <summary>
        /// Not set value (default seriliaztion)
        /// </summary>
        Undefined = 0,
        
        /// <summary>
        /// Reverse all chars order
        /// </summary>
        ReverseChars = 1,
        
        /// <summary>
        /// Reverse word order
        /// </summary>
        ReverseWordOrder = 2
    }
}