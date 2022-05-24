using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class ListResourceRes
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public PaginatedResources Data { get; set; }
    }
}