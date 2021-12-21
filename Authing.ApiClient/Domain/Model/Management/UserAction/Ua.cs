using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class Ua
    {
        [JsonProperty("build")]
        public string Build { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("patch")]
        public string Patch { get; set; }

        [JsonProperty("osMinor")]
        public string OsMinor { get; set; }

        [JsonProperty("osMajor")]
        public string OsMajor { get; set; }

        [JsonProperty("osName")]
        public string OsName { get; set; }

        [JsonProperty("minor")]
        public string Minor { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("major")]
        public string Major { get; set; }
    }
}
