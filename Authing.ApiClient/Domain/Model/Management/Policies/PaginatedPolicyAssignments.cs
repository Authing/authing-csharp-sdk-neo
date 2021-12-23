using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PaginatedPolicyAssignments
    {
        #region members
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<PolicyAssignment> List { get; set; }
        #endregion
    }
}
