using System.Collections.Generic;

namespace HomeAssignment.Task4.Contracts.DTO
{
    /// <summary>
    /// Asset with list of markets
    /// </summary>
    public class AssetsWithPrices
    {
        /// <summary>
        /// Asset record
        /// </summary>
        public AssetsType Asset { get; set; }
        
        /// <summary>
        /// Collection of markets
        /// </summary>
        public List<MarketType> Markets { get; set; }
    }
}