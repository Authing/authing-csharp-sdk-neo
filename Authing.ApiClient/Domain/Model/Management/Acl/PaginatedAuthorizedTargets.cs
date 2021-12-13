using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class PaginatedAuthorizedTargets
    {
        #region members
        [JsonProperty("list")]
        public IEnumerable<ResourcePermissionAssignment> List { get; set; }

        [JsonProperty("totalCount")]
        public int? TotalCount { get; set; }
        #endregion
    }
}