using System;
using Newtonsoft.Json;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class Resources
    {
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("actions")]
        public ResourceAction[] Actions { get; set; }

        [JsonProperty("type")]
        public ResourceType Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("namespaceId")]
        public string NameSpaceId { get; set; }

        [JsonProperty("apiIdentifier")]
        public string ApiIdentifier { get; set; }
    }
}
