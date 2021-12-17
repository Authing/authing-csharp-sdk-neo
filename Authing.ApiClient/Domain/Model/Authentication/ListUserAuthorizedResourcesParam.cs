using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ListUserAuthorizedResourcesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

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

        public ListUserAuthorizedResourcesParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// ListUserAuthorizedResourcesParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resourceType=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ListUserAuthorizedResourcesDocument,
                OperationName = "listUserAuthorizedResources",
                Variables = this
            };
        }


        public static string ListUserAuthorizedResourcesDocument = @"
        query listUserAuthorizedResources($id: String!, $namespace: String, $resourceType: String) {
          user(id: $id) {
            authorizedResources(namespace: $namespace, resourceType: $resourceType) {
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
