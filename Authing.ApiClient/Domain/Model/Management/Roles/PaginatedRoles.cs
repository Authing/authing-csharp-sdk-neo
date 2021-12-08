using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class PaginatedRoles
    {
        #region members
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<Role> List { get; set; }
        #endregion
    }
}
