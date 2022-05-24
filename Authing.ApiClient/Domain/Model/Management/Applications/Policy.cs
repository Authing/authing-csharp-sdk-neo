using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    #region Policy
    public class Policy
    {
        #region members
        /// <summary>
        /// 权限组 code
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("statements")]
        public IEnumerable<PolicyStatement> Statements { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// 被授权次数
        /// </summary>
        [JsonProperty("assignmentsCount")]
        public int AssignmentsCount { get; set; }

        /// <summary>
        /// 授权记录
        /// </summary>
        [JsonProperty("assignments")]
        public IEnumerable<PolicyAssignment> Assignments { get; set; }
        #endregion
    }
    #endregion

    #region PolicyStatement

    #endregion

    #region PolicyStatementCondition

    #endregion

    #region PolicyAssignment

    #endregion
}
