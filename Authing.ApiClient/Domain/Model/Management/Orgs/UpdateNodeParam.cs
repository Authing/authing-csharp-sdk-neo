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
    public class UpdateNodeParam
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

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("includeChildrenNodes")]
        public bool? IncludeChildrenNodes { get; set; }

        /// <summary>
        /// 节点唯一标志
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 节点名称
        /// Optional
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 节点唯一标志
        /// Optional
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 节点描述信息
        /// Optional
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        public UpdateNodeParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// UpdateNodeParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int), sortBy=(SortByEnum), includeChildrenNodes=(bool), name=(string), code=(string), description=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateNodeDocument,
                OperationName = "updateNode",
                Variables = this
            };
        }


        public static string UpdateNodeDocument = @"
        mutation updateNode($page: Int, $limit: Int, $sortBy: SortByEnum, $includeChildrenNodes: Boolean, $id: String!, $name: String, $code: String, $description: String) {
          updateNode(id: $id, name: $name, code: $code, description: $description) {
            id
            orgId
            name
            nameI18n
            description
            descriptionI18n
            order
            code
            root
            depth
            path
            createdAt
            updatedAt
            children
            users(page: $page, limit: $limit, sortBy: $sortBy, includeChildrenNodes: $includeChildrenNodes) {
              totalCount
            }
          }
        }
        ";
    }
}
