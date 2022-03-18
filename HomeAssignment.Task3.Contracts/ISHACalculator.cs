namespace HomeAssignment.Contracts
{
    /// <summary>
    /// Calculate Calculate a SHA hash (in hex form)
    /// </summary>
    public interface ISHACalculator
    {
        string Calc(string fileUrl);
        
        
        //https://speed.hetzner.de/100MB.bin
        //https://speed.hetzner.de/1GB.bin
        //https://speed.hetzner.de/10GB.bin
    }
}