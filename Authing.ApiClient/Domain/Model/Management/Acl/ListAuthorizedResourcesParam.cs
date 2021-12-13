using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ListAuthorizedResourcesParam
    {
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]

        public PolicyAssignmentTargetType TargetType { get; set; }
        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }
        [JsonProperty("namespace")]
        public string NameSpace { get; set; }
        [JsonProperty("resourceType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType Type { get; set; }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AuthorizedResourcesDocument,
                OperationName = "authorizedResources",
                Variables = this
            };
        }

        public ListAuthorizedResourcesParam(PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace, ResourceType type)
        {
            TargetType = targetType;
            TargetIdentifier = targetIdentifier;
            NameSpace = nameSpace;
            Type = type;
        }

        public static string AuthorizedResourcesDocument = @"
        query authorizedResources($targetType: PolicyAssignmentTargetType, $targetIdentifier: String, $namespace: String, $resourceType: String) {
      authorizedResources(targetType: $targetType, targetIdentifier: $targetIdentifier, namespace: $namespace, resourceType: $resourceType) {
        totalCount
        list {
          code
          type
          actions
        }
      }
    }
";

    }
}