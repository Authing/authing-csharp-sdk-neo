using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class OrgsParam
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
        [JsonProperty("sortBy")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortByEnum? SortBy { get; set; }

        public OrgsParam()
        {

        }
        /// <summary>
        /// OrgsParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int), sortBy=(SortByEnum) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = OrgsDocument,
                OperationName = "orgs",
                Variables = this
            };
        }


        public static string OrgsDocument = @"
        query orgs($page: Int, $limit: Int, $sortBy: SortByEnum) {
          orgs(page: $page, limit: $limit, sortBy: $sortBy) {
            totalCount
            list {
              id
              rootNode {
                id
                name
                nameI18n
                path
                description
                descriptionI18n
                order
                code
                root
                depth
                createdAt
                updatedAt
                children
              }
              nodes {
                id
                name
                path
                nameI18n
                description
                descriptionI18n
                order
                code
                root
                depth
                createdAt
                updatedAt
                children
              }
            }
          }
        }
        ";
    }
}
