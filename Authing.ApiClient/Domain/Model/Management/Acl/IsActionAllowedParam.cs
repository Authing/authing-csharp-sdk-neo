using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class IsActionAllowedParam
    {
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public IsActionAllowedParam(string resource, string action, string userId, string ns="")
        {
            Resource = resource;
            Action = action;
            UserId = userId;
            Namespace = ns;
        }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = IsActionAllowedDocument,
                OperationName = "isActionAllowed",
                Variables = this
            };
        }

        public static string IsActionAllowedDocument = @"
        query isActionAllowed($resource: String!, $action: String!, $userId: String!, $namespace: String) {
          isActionAllowed(resource: $resource, action: $action, userId: $userId, namespace: $namespace)
        }
        ";
    }
}