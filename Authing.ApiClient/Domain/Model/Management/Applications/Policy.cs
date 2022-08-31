using System;
using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    #region Policy
    public class PolicyOld
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

    public class Policy
    {
        public DateTime assignedAt { get; set; }
        public object inheritByChildren { get; set; }
        public bool enabled { get; set; }
        public string policyId { get; set; }
        public string code { get; set; }
        public PolicyInfo policy { get; set; }
        public string targetNamespace { get; set; }
        public PolicyAssignmentTargetType targetType { get; set; }
        public string targetIdentifier { get; set; }
        public Target target { get; set; }
        public string _namespace { get; set; }
        public string namespaceName { get; set; }
        public string namesapceDes { get; set; }
    }

    public class PolicyInfo
    {
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string userPoolId { get; set; }
        public bool isDefault { get; set; }
        public bool isAuto { get; set; }
        public bool hidden { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Statement[] statements { get; set; }
        public int namespaceId { get; set; }
    }

    public class Statement
    {
        public object condition { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string userPoolId { get; set; }
        public string policyId { get; set; }
        public string resource { get; set; }
        public object resourceType { get; set; }
        public string[] actions { get; set; }
        public string effect { get; set; }
    }

    public class Target
    {
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string userPoolId { get; set; }
        public string code { get; set; }
        public object description { get; set; }
        public object parentCode { get; set; }
        public bool isSystem { get; set; }
        public int namespaceId { get; set; }
    }

}
