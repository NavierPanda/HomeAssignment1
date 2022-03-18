using System;

namespace HomeAssignment.WebApi.Options
{
    public class StaticStringResourcesOptions
    {
        public const string ConfigSectionKey = "StaticStringResources";

        public string Lorem { get; set; } = String.Empty;
    }
}