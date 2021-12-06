using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class AddNodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        /// <summary>
        /// 父节点 ID
        /// Optional
        /// </summary>
        [JsonProperty("parentNodeId")]
        public string ParentNodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// Required
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 节点名称，国际化
        /// Optional
        /// </summary>
        [JsonProperty("nameI18n")]
        public string NameI18n { get; set; }

        /// <summary>
        /// 节点描述信息
        /// Optional
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 节点描述信息,国际化
        /// Optional
        /// </summary>
        [JsonProperty("descriptionI18n")]
        public string DescriptionI18n { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("order")]
        public int? Order { get; set; }

        /// <summary>
        /// 节点唯一标志
        /// Optional
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        public AddNodeParam(string orgId, string name)
        {
            this.OrgId = orgId;
            this.Name = name;
        }
        /// <summary>
        /// AddNodeParam.Request 
        /// <para>Required variables:<br/> { orgId=(string), name=(string) }</para>
        /// <para>Optional variables:<br/> { parentNodeId=(string), nameI18n=(string), description=(string), descriptionI18n=(string), order=(int), code=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AddNodeDocument,
                OperationName = "addNode",
                Variables = this
            };
        }


        public static string AddNodeDocument = @"
        mutation addNode($orgId: String!, $parentNodeId: String, $name: String!, $nameI18n: String, $description: String, $descriptionI18n: String, $order: Int, $code: String) {
          addNode(orgId: $orgId, parentNodeId: $parentNodeId, name: $name, nameI18n: $nameI18n, description: $description, descriptionI18n: $descriptionI18n, order: $order, code: $code) {
            id
            rootNode {
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
            }
            nodes {
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
            }
          }
        }
        ";
    }
}
