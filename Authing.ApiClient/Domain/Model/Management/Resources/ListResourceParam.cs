using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class ListResourceParam
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("type")]
        public ResourceType? Type { get; set; }
    }
}