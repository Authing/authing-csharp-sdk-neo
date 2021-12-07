using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class ListGroupAuthorizedResourcesParam
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

        public ListGroupAuthorizedResourcesParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// ListGroupAuthorizedResourcesParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resourceType=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ListGroupAuthorizedResourcesDocument,
                OperationName = "listGroupAuthorizedResources",
                Variables = this
            };
        }


        public static string ListGroupAuthorizedResourcesDocument = @"
        query listGroupAuthorizedResources($code: String!, $namespace: String, $resourceType: String) {
          group(code: $code) {
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