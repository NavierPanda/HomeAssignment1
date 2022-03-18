namespace HomeAssignment.Task4.Contracts.DTO
{
    /// <summary>
    /// Market with symbol and recorded exchange price
    /// </summary>
    public class MarketType
    {
        /// <summary>
        /// Market symbol
        /// </summary>
        public string MarketSymbol { get; set; }
        
        /// <summary>
        /// Last recorded exchange price
        /// </summary>
        public TickerType Ticker { get; set; }
    }
}