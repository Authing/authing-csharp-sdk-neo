using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class DeleteRoleParam
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

        public DeleteRoleParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// DeleteRoleParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteRoleDocument,
                OperationName = "deleteRole",
                Variables = this
            };
        }


        public static string DeleteRoleDocument = @"
        mutation deleteRole($code: String!, $namespace: String) {
          deleteRole(code: $code, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}
