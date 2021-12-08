using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class PaginatedUsers
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
        [JsonProperty("list")]
        public IEnumerable<User> List { get; set; }
    }
}