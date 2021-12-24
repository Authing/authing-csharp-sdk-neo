using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class DeletePoliciesParam
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

        public DeletePoliciesParam(IEnumerable<string> codeList)
        {
            this.CodeList = codeList;
        }
        /// <summary>
        /// DeletePoliciesParam.Request 
        /// <para>Required variables:<br/> { codeList=(string[]) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeletePoliciesDocument,
                OperationName = "deletePolicies",
                Variables = this
            };
        }


        public static string DeletePoliciesDocument = @"
        mutation deletePolicies($codeList: [String!]!, $namespace: String) {
          deletePolicies(codeList: $codeList, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}
