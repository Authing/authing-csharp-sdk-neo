using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class UpdateExtIdpConnectionOption
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, object> Fields { get; set; }

        [JsonProperty("userMatchFields")]
        public IEnumerable<string> UserMatchFields { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}