using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class RoleParam
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

        public RoleParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// RoleParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RoleDocument,
                OperationName = "role",
                Variables = this
            };
        }


        public static string RoleDocument = @"
        query role($code: String!, $namespace: String) {
          role(code: $code, namespace: $namespace) {
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
