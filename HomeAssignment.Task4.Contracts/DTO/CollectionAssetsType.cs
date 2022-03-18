using System.Collections.Generic;

namespace HomeAssignment.Task4.Contracts.DTO
{
    /// <summary>
    /// Available assets response
    /// </summary>
    public class CollectionAssetsType
    {
        /// <summary>
        /// All available assets
        /// </summary>
        public List<AssetsType> Assets { get; set; }
    }
}