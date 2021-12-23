using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class UpdatePolicyParam
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
        [JsonProperty("statements")]
        public IEnumerable<PolicyStatementInput> Statements { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("newCode")]
        public string NewCode { get; set; }

        public UpdatePolicyParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// UpdatePolicyParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), description=(string), statements=(PolicyStatementInput[]), newCode=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdatePolicyDocument,
                OperationName = "updatePolicy",
                Variables = this
            };
        }


        public static string UpdatePolicyDocument = @"
        mutation updatePolicy($namespace: String, $code: String!, $description: String, $statements: [PolicyStatementInput!], $newCode: String) {
          updatePolicy(namespace: $namespace, code: $code, description: $description, statements: $statements, newCode: $newCode) {
            namespace
            code
            description
            statements {
              resource
              actions
              effect
              condition {
                param
                operator
                value
              }
            }
            createdAt
            updatedAt
          }
        }
        ";
    }
}
