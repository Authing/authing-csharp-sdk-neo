using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class DeleteRolesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("codeList")]
        public IEnumerable<string> CodeList { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public DeleteRolesParam(IEnumerable<string> codeList)
        {
            this.CodeList = codeList;
        }
        /// <summary>
        /// DeleteRolesParam.Request 
        /// <para>Required variables:<br/> { codeList=(string[]) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteRolesDocument,
                OperationName = "deleteRoles",
                Variables = this
            };
        }


        public static string DeleteRolesDocument = @"
        mutation deleteRoles($codeList: [String!]!, $namespace: String) {
          deleteRoles(codeList: $codeList, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}
