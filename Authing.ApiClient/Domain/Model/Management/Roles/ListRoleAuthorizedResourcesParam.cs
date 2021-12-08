using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class ListRoleAuthorizedResourcesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        public ListRoleAuthorizedResourcesParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// ListRoleAuthorizedResourcesParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resourceType=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ListRoleAuthorizedResourcesDocument,
                OperationName = "listRoleAuthorizedResources",
                Variables = this
            };
        }


        public static string ListRoleAuthorizedResourcesDocument = @"
        query listRoleAuthorizedResources($code: String!, $namespace: String, $resourceType: String) {
          role(code: $code, namespace: $namespace) {
            authorizedResources(resourceType: $resourceType) {
              totalCount
              list {
                code
                type
                actions
              }
            }
          }
        }
        ";
    }
}
