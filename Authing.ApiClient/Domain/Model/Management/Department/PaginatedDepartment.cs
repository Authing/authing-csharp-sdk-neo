using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Department
{
    #region PaginatedDepartments
    public class PaginatedDepartments
    {
        #region members
        [JsonProperty("list")]
        public IEnumerable<UserDepartment> List { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
        #endregion
    }
    #endregion
}
