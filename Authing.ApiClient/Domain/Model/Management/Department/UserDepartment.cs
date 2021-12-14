using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Domain.Model.Management.Orgs;

namespace Authing.ApiClient.Domain.Model.Management.Department
{
    #region UserDepartment
    public class UserDepartment
    {
        #region members
        [JsonProperty("department")]
        public Node Department { get; set; }

        /// <summary>
        /// 是否为主部门
        /// </summary>
        [JsonProperty("isMainDepartment")]
        public bool IsMainDepartment { get; set; }

        /// <summary>
        /// 加入该部门的时间
        /// </summary>
        [JsonProperty("joinedAt")]
        public string JoinedAt { get; set; }
        #endregion
    }
    #endregion
}
