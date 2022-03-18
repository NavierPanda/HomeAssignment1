namespace HomeAssignment.Task3.Contracts
{
    /// <summary>
    /// SHA calculation service
    /// </summary>
    public interface ISHACalcService
    {
        /// <summary>
        /// Calc SHA hash string from file url
        /// </summary>
        /// <param name="fileUrl"></param>
        string Calc(string fileUrl);
    }
}