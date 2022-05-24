using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class ResourceAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}