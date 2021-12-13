using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class Pagination<T>
    {
        [JsonProperty("list")]
        public IEnumerable<T> List { get; set; }

        [JsonProperty("totalcount")]
        public int totalCount { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

    }
}