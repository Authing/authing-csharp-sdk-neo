using System;
using Newtonsoft.Json;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class ApplicationAccessPolicies
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public Policy[] List { get; set; }
    }

    public class AppAccessPolicyQueryFilter
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }

    public class AppAccessPolicy
    {
        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifiers")]
        public string[] TartgetIdentifiers { get; set; }

        [JsonProperty("namespace")]
        public string NameSpace { get; set; }

        [JsonProperty("inheritByChildren")]
        public bool InheritByChildren { get; set; }
    }

    public class UpdateDefaultApplicationAccessPolicyParam
    {
        public DefaultStrategyEnum DefaultStrategy { get; set; }
    }
}
