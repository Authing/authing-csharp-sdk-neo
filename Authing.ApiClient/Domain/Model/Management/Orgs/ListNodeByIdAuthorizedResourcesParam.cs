using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class ListNodeByIdAuthorizedResourcesParam
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

        public ListNodeByIdAuthorizedResourcesParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// ListNodeByIdAuthorizedResourcesParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resourceType=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ListNodeByIdAuthorizedResourcesDocument,
                OperationName = "listNodeByIdAuthorizedResources",
                Variables = this
            };
        }


        public static string ListNodeByIdAuthorizedResourcesDocument = @"
        query listNodeByIdAuthorizedResources($id: String!, $namespace: String, $resourceType: String) {
          nodeById(id: $id) {
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
