using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class Node
    {
        #region members
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构 ID
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 多语言名称，**key** 为标准 **i18n** 语言编码，**value** 为对应语言的名称。
        /// </summary>
        [JsonProperty("nameI18n")]
        public string NameI18n { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 多语言描述信息
        /// </summary>
        [JsonProperty("descriptionI18n")]
        public string DescriptionI18n { get; set; }

        /// <summary>
        /// 在父节点中的次序值。**order** 值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        [JsonProperty("order")]
        public int? Order { get; set; }

        /// <summary>
        /// 节点唯一标志码，可以通过 code 进行搜索
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 是否为根节点
        /// </summary>
        [JsonProperty("root")]
        public bool? Root { get; set; }

        /// <summary>
        /// 距离父节点的深度（如果是查询整棵树，返回的 **depth** 为距离根节点的深度，如果是查询某个节点的子节点，返回的 **depth** 指的是距离该节点的深度。）
        /// </summary>
        [JsonProperty("depth")]
        public int? Depth { get; set; }

        [JsonProperty("path")]
        public IEnumerable<string> Path { get; set; }

        [JsonProperty("codePath")]
        public IEnumerable<string> CodePath { get; set; }

        [JsonProperty("namePath")]
        public IEnumerable<string> NamePath { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// 该节点的子节点 **ID** 列表
        /// </summary>
        [JsonProperty("children")]
        public IEnumerable<string> Children { get; set; }

        /// <summary>
        /// 节点的用户列表
        /// </summary>
        [JsonProperty("users")]
        public PaginatedUsers Users { get; set; }

        /// <summary>
        /// 被授权访问的所有资源
        /// </summary>
        [JsonProperty("authorizedResources")]
        public PaginatedAuthorizedResources AuthorizedResources { get; set; }
        #endregion
    }
}
