using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class Role
    {
        #region members
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 权限组 code
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// 唯一标志 code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 资源描述符 arn
        /// </summary>
        [JsonProperty("arn")]
        public string Arn { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否为系统内建，系统内建的角色不能删除
        /// </summary>
        [JsonProperty("isSystem")]
        public bool? IsSystem { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// 被授予此角色的用户列表
        /// </summary>
        [JsonProperty("users")]
        public PaginatedUsers Users { get; set; }

        /// <summary>
        /// 被授权访问的所有资源
        /// </summary>
        [JsonProperty("authorizedResources")]
        public PaginatedAuthorizedResources AuthorizedResources { get; set; }

        /// <summary>
        /// 父角色
        /// </summary>
        [JsonProperty("parent")]
        public Role Parent { get; set; }
        #endregion
    }
}