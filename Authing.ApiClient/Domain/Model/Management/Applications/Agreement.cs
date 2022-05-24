using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class Agreement
    {
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
