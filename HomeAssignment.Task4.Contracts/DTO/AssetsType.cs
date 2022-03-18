namespace HomeAssignment.Task4.Contracts.DTO
{
    /// <summary>
    /// Asset record type
    /// </summary>
    public class AssetsType
    {
        /// <summary>
        /// Requested asset name
        /// </summary>
        public string AssetName { get; set; }
        
        /// <summary>
        /// Requested asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }
        
        /// <summary>
        /// Known market cap
        /// </summary>
        public long? MarketCap { get; set; }
    }
}