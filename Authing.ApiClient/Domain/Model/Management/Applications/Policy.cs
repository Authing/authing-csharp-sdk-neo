using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Types;

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
    public class PolicyStatement
    {
        #region members
        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }

        [JsonProperty("effect")]
        public PolicyEffect? Effect { get; set; }

        [JsonProperty("condition")]
        public IEnumerable<PolicyStatementCondition> Condition { get; set; }
        #endregion
    }
    #endregion

    #region PolicyStatementCondition
    public class PolicyStatementCondition
    {
        #region members
        [JsonProperty("param")]
        public string Param { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
        #endregion
    }
    #endregion

    #region PolicyAssignment
    public class PolicyAssignment
    {
        #region members
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }
        #endregion
    }
    #endregion
}
