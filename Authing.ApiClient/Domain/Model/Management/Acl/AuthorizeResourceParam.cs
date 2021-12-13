using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AuthorizeResourceParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("resource")]
        public string Resource { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("resourceType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType? ResourceType { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("opts")]
        public IEnumerable<AuthorizeResourceOpt> Opts { get; set; }

        public AuthorizeResourceParam()
        {

        }
        /// <summary>
        /// AuthorizeResourceParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { namespace=(string), resource=(string), resourceType=(ResourceType), opts=(AuthorizeResourceOpt[]) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AuthorizeResourceDocument,
                OperationName = "authorizeResource",
                Variables = this
            };
        }


        public static string AuthorizeResourceDocument = @"
        mutation authorizeResource($namespace: String, $resource: String, $resourceType: ResourceType, $opts: [AuthorizeResourceOpt]) {
          authorizeResource(namespace: $namespace, resource: $resource, resourceType: $resourceType, opts: $opts) {
            code
            message
          }
        }
        ";
    }
}