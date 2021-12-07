using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class GroupsParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

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

        public GroupsParam()
        {

        }
        /// <summary>
        /// GroupsParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { userId=(string), page=(int), limit=(int), sortBy=(SortByEnum) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GroupsDocument,
                OperationName = "groups",
                Variables = this
            };
        }


        public static string GroupsDocument = @"
        query groups($userId: String, $page: Int, $limit: Int, $sortBy: SortByEnum) {
          groups(userId: $userId, page: $page, limit: $limit, sortBy: $sortBy) {
            totalCount
            list {
              code
              name
              description
              createdAt
              updatedAt
            }
          }
        }
        ";
    }

}
