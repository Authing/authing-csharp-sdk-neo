using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class ListNodeByCodeAuthorizedResourcesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

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

        public ListNodeByCodeAuthorizedResourcesParam(string orgId, string code)
        {
            this.OrgId = orgId;
            this.Code = code;
        }
        /// <summary>
        /// ListNodeByCodeAuthorizedResourcesParam.Request 
        /// <para>Required variables:<br/> { orgId=(string), code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resourceType=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ListNodeByCodeAuthorizedResourcesDocument,
                OperationName = "listNodeByCodeAuthorizedResources",
                Variables = this
            };
        }


        public static string ListNodeByCodeAuthorizedResourcesDocument = @"
        query listNodeByCodeAuthorizedResources($orgId: String!, $code: String!, $namespace: String, $resourceType: String) {
          nodeByCode(orgId: $orgId, code: $code) {
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
