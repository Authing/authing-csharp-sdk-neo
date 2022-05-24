using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class PaginatedOrgsAndNodes
    {
        #region members
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<IEnumerable<OrgAndNode>> List { get; set; }
        #endregion
    }
}