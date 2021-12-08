using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class CreateRoleParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        public CreateRoleParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// CreateRoleParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), description=(string), parent=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CreateRoleDocument,
                OperationName = "createRole",
                Variables = this
            };
        }


        public static string CreateRoleDocument = @"
        mutation createRole($namespace: String, $code: String!, $description: String, $parent: String) {
          createRole(namespace: $namespace, code: $code, description: $description, parent: $parent) {
            id
            namespace
            code
            arn
            description
            createdAt
            updatedAt
            parent {
              namespace
              code
              arn
              description
              createdAt
              updatedAt
            }
          }
        }
        ";
    }
}
