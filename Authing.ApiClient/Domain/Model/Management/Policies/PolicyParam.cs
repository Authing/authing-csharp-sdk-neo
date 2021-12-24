using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PolicyParam
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

        public PolicyParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// PolicyParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = PolicyDocument,
                OperationName = "policy",
                Variables = this
            };
        }


        public static string PolicyDocument = @"
        query policy($namespace: String, $code: String!) {
          policy(code: $code, namespace: $namespace) {
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
          }
        }
        ";
    }
}
