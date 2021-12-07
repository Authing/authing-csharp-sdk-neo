using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class Group
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdAt")]
        public string CreateAt { get; set; }

        [JsonProperty("updateAt")]
        public string UpdateAt { get; set; }

        [JsonProperty("users")]
        public PaginatedUsers Users { get; set; }

        [JsonProperty("authorizedResources")]
        public PaginatedAuthorizedResources AuthorizedResources { get; set; }
    }
}
