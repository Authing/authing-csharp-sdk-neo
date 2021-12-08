using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class RolesParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

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
        [JsonProperty("sortBy")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortByEnum? SortBy { get; set; }

        public RolesParam()
        {

        }
        /// <summary>
        /// RolesParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { namespace=(string), page=(int), limit=(int), sortBy=(SortByEnum) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RolesDocument,
                OperationName = "roles",
                Variables = this
            };
        }


        public static string RolesDocument = @"
        query roles($namespace: String, $page: Int, $limit: Int, $sortBy: SortByEnum) {
          roles(namespace: $namespace, page: $page, limit: $limit, sortBy: $sortBy) {
            totalCount
            list {
              id
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
