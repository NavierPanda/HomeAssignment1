namespace HomeAssignment.WebApi.Contracts
{
    /// <summary>
    /// Predefined file urls
    /// </summary>
    public enum PredefinedFileUrlEnum
    {
        /// <summary>
        /// Not set value (default seriliaztion)
        /// </summary>
        Undefined = 0,
        
        /// <summary>
        /// 100 mb file
        /// </summary>
        File100Mb = 1,
        
        /// <summary>
        /// 1 gb file
        /// </summary>
        File1Gb = 2,
        
        /// <summary>
        /// 10 gb file
        /// </summary>
        File10Gb = 3,
    }
}