using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class PaginatedAuthorizedResources
    {
        [JsonProperty("totalCount")]
        public int totalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<AuthorizedResource> List { get; set; }
    }
}