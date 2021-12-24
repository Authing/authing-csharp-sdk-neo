using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PoliciesParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public PoliciesParam()
        {

        }
        /// <summary>
        /// PoliciesParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int), namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = PoliciesDocument,
                OperationName = "policies",
                Variables = this
            };
        }


        public static string PoliciesDocument = @"
        query policies($page: Int, $limit: Int, $namespace: String) {
          policies(page: $page, limit: $limit, namespace: $namespace) {
            totalCount
            list {
              namespace
              code
              description
              createdAt
              updatedAt
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
            }
          }
        }
        ";
    }
}
