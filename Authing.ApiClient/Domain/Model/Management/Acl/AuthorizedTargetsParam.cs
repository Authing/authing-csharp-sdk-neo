using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AuthorizedTargetsParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("resourceType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("resource")]
        public string Resource { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PolicyAssignmentTargetType? TargetType { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("actions")]
        public AuthorizedTargetsActionsInput Actions { get; set; }

        public AuthorizedTargetsParam(string nameSpace, ResourceType resourceType, string resource)
        {
            this.Namespace = nameSpace;
            this.ResourceType = resourceType;
            this.Resource = resource;
        }
        /// <summary>
        /// AuthorizedTargetsParam.Request 
        /// <para>Required variables:<br/> { namespace=(string), resourceType=(ResourceType), resource=(string) }</para>
        /// <para>Optional variables:<br/> { targetType=(PolicyAssignmentTargetType), actions=(AuthorizedTargetsActionsInput) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AuthorizedTargetsDocument,
                OperationName = "authorizedTargets",
                Variables = this
            };
        }


        public static string AuthorizedTargetsDocument = @"
        query authorizedTargets($namespace: String!, $resourceType: ResourceType!, $resource: String!, $targetType: PolicyAssignmentTargetType, $actions: AuthorizedTargetsActionsInput) {
          authorizedTargets(namespace: $namespace, resource: $resource, resourceType: $resourceType, targetType: $targetType, actions: $actions) {
            totalCount
            list {
              targetType
              targetIdentifier
              actions
            }
          }
        }
        ";
    }
}