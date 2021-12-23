using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
   public class EnableAssignmentParam
    {
       [JsonProperty("policy")]
        public string Policy { get; set; }
        [JsonProperty("targetType")] 
        public PolicyAssignmentTargetType TargetType { get; set; }
        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }
        [JsonProperty("namespace")] 
        public string NameSpace { get; set; }

        public EnableAssignmentParam()
        {

        }

        public EnableAssignmentParam(string policy,PolicyAssignmentTargetType targetType,string targetIdentifier)
        {

        }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = EnableAssignmentDocument,
                OperationName = "enableAssignmentDocument",
                Variables = this
            };
        }

        private string EnableAssignmentDocument = @"
        mutation enablePolicyAssignment(${ '$'}
        policy: String!, ${'$'}targetType: PolicyAssignmentTargetType!, ${'$'}targetIdentifier: String!, ${'$'}namespace: String) {
      enablePolicyAssignment(policy: ${ '$'}
        policy, targetType: ${'$'}targetType, targetIdentifier: ${'$'}targetIdentifier, namespace: ${'$'}namespace) {
        message
        code
      }
}
";
    }
}
