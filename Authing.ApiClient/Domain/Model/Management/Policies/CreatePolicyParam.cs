using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class CreatePolicyParam
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
        /// Required
        /// </summary>
        [JsonProperty("statements")]
        public IEnumerable<PolicyStatementInput> Statements { get; set; }

        public CreatePolicyParam(string code, IEnumerable<PolicyStatementInput> statements)
        {
            this.Code = code;
            this.Statements = statements;
        }
        /// <summary>
        /// CreatePolicyParam.Request 
        /// <para>Required variables:<br/> { code=(string), statements=(PolicyStatementInput[]) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), description=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CreatePolicyDocument,
                OperationName = "createPolicy",
                Variables = this
            };
        }


        public static string CreatePolicyDocument = @"
        mutation createPolicy($namespace: String, $code: String!, $description: String, $statements: [PolicyStatementInput!]!) {
          createPolicy(namespace: $namespace, code: $code, description: $description, statements: $statements) {
            namespace
            code
            isDefault
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
            assignmentsCount
          }
        }
        ";
    }
}
