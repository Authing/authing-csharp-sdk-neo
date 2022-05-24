using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class PaginatedResources
    {
        #region members
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<Resources> List { get; set; }
        #endregion
    }
}