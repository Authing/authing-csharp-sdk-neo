using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class AuthorizedResource
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType Type { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }

        [JsonProperty("apiIdentifier")]
        public string ApiIdentifier { get; set; }
    }
}