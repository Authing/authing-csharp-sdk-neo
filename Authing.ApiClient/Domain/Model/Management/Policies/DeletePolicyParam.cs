using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class DeletePolicyParam
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

        public DeletePolicyParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// DeletePolicyParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeletePolicyDocument,
                OperationName = "deletePolicy",
                Variables = this
            };
        }


        public static string DeletePolicyDocument = @"
        mutation deletePolicy($code: String!, $namespace: String) {
          deletePolicy(code: $code, namespace: $namespace) {
            message
            code
          }
        }
        ";
    }
}
