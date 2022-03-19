using System;

namespace HomeAssignment.WebApi.Options
{
    /// <summary>
    /// Options of string inverter
    /// </summary>
    public class StaticStringResourcesOptions
    {
        public const string ConfigSectionKey = "StaticStringResources";

        public string Lorem { get; set; } = String.Empty;
    }
}