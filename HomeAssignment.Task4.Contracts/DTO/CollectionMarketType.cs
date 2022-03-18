using System.Collections.Generic;

namespace HomeAssignment.Task4.Contracts.DTO
{
    /// <summary>
    /// Available markets response
    /// </summary>
    public class CollectionMarketType
    {
        /// <summary>
        /// All available markets
        /// </summary>
        public List<MarketType> Markets { get; set; }
    }
}