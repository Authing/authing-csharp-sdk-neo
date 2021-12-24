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
    public class AddPolicyAssignmentsParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("policies")]
        public IEnumerable<string> Policies { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PolicyAssignmentTargetType TargetType { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("targetIdentifiers")]
        public IEnumerable<string> TargetIdentifiers { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("inheritByChildren")]
        public bool? InheritByChildren { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public AddPolicyAssignmentsParam(IEnumerable<string> policies, PolicyAssignmentTargetType targetType)
        {
            this.Policies = policies;
            this.TargetType = targetType;
        }
        /// <summary>
        /// AddPolicyAssignmentsParam.Request 
        /// <para>Required variables:<br/> { policies=(string[]), targetType=(PolicyAssignmentTargetType) }</para>
        /// <para>Optional variables:<br/> { targetIdentifiers=(string[]), inheritByChildren=(bool), namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AddPolicyAssignmentsDocument,
                OperationName = "addPolicyAssignments",
                Variables = this
            };
        }


        public static string AddPolicyAssignmentsDocument = @"
        mutation addPolicyAssignments($policies: [String!]!, $targetType: PolicyAssignmentTargetType!, $targetIdentifiers: [String!], $inheritByChildren: Boolean, $namespace: String) {
          addPolicyAssignments(policies: $policies, targetType: $targetType, targetIdentifiers: $targetIdentifiers, inheritByChildren: $inheritByChildren, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}
