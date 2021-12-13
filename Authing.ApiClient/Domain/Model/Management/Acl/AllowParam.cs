using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AllowParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("resource")]
        public string Resource { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("userIds")]
        public IEnumerable<string> UserIds { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("roleCode")]
        public string RoleCode { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("roleCodes")]
        public IEnumerable<string> RoleCodes { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public AllowParam(string resource, string action)
        {
            this.Resource = resource;
            this.Action = action;
        }
        /// <summary>
        /// AllowParam.Request 
        /// <para>Required variables:<br/> { resource=(string), action=(string) }</para>
        /// <para>Optional variables:<br/> { userId=(string), userIds=(string[]), roleCode=(string), roleCodes=(string[]), namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AllowDocument,
                OperationName = "allow",
                Variables = this
            };
        }


        public static string AllowDocument = @"
        mutation allow($resource: String!, $action: String!, $userId: String, $userIds: [String!], $roleCode: String, $roleCodes: [String!], $namespace: String) {
          allow(resource: $resource, action: $action, userId: $userId, userIds: $userIds, roleCode: $roleCode, roleCodes: $roleCodes, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}