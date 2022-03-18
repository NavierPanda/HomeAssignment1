namespace HomeAssignment.Contracts
{
    /// <summary>
    /// Check if url is valid
    /// </summary>
    public interface IUrlValidator
    {
        /// <summary>
        /// Check if url is valid
        /// </summary>
        /// <param name="fileUrl">file url</param>
        bool IsValidFileUrl(string fileUrl);
    }
}