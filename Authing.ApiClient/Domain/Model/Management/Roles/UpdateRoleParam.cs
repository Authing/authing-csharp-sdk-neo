using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class UpdateRoleParam
    {

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
        [JsonProperty("newCode")]
        public string NewCode { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public UpdateRoleParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// UpdateRoleParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { description=(string), newCode=(string), namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateRoleDocument,
                OperationName = "updateRole",
                Variables = this
            };
        }


        public static string UpdateRoleDocument = @"
        mutation updateRole($code: String!, $description: String, $newCode: String, $namespace: String) {
          updateRole(code: $code, description: $description, newCode: $newCode, namespace: $namespace) {
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
