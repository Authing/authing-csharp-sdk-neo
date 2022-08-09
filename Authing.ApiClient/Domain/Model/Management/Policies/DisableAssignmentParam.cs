using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class DisableAssignmentParam
    {
        [JsonProperty("policy")]
        public string Policy { get; set; }

        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }

        [JsonProperty("namespace")]
        public string NameSpace { get; set; }

        public DisableAssignmentParam()
        {

        }

        public DisableAssignmentParam(string policy,PolicyAssignmentTargetType  targetType)
        {
            Policy = policy;
            TargetType = targetType;
        }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DisableAssignmentDocument,
                OperationName = "disbalePolicyAssignment",
                Variables = this
            };
        }

        private string DisableAssignmentDocument = @"
    mutation disbalePolicyAssignment($policy: String!, $targetType: PolicyAssignmentTargetType!, $targetIdentifier: String!, $namespace: String) {
  disbalePolicyAssignment(policy: $policy, targetType: $targetType, targetIdentifier: $targetIdentifier, namespace: $namespace) {
    message
    code
  }
}";

    }
}
