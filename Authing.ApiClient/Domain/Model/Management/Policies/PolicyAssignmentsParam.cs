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
    public class PolicyAssignmentsParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PolicyAssignmentTargetType? TargetType { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        public PolicyAssignmentsParam()
        {

        }
        /// <summary>
        /// PolicyAssignmentsParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { namespace=(string), code=(string), targetType=(PolicyAssignmentTargetType), targetIdentifier=(string), page=(int), limit=(int) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = PolicyAssignmentsDocument,
                OperationName = "policyAssignments",
                Variables = this
            };
        }


        public static string PolicyAssignmentsDocument = @"
        query policyAssignments($namespace: String, $code: String, $targetType: PolicyAssignmentTargetType, $targetIdentifier: String, $page: Int, $limit: Int) {
          policyAssignments(namespace: $namespace, code: $code, targetType: $targetType, targetIdentifier: $targetIdentifier, page: $page, limit: $limit) {
            totalCount
            list {
              code
              targetType
              targetIdentifier
            }
          }
        }
        ";
    }
}
