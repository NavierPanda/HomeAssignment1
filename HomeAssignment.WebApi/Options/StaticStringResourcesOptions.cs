using System;

namespace HomeAssignment.WebApi.Options
{
    /// <summary>
    /// Options of string inverter
    /// </summary>
    public class StaticStringResourcesOptions
    {
        /// <summary>
        /// appsettings section
        /// </summary>
        public const string ConfigSectionKey = "StaticStringResources";

        /// <summary>
        /// Lorem string
        /// </summary>
        public string Lorem { get; set; } = String.Empty;
    }
}