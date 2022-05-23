using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class ListOrgsResponse
    {
        #region members
        /// <summary>
        /// 可读的接口响应说明，请以业务状态码 code 作为判断业务是否成功的标志
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 业务状态码（与 HTTP 响应码不同），但且仅当为 200 的时候表示操作成功表示，详细说明请见：
        /// [Authing 错误代码列表](https://docs.authing.co/advanced/error-code.html)
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<OrgAndNode> Data { get; set; }
        #endregion
    }

    public class OrgAndNode
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        [JsonProperty("rootNodeId")]
        public string RootNodeId { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameI18n")]
        public NameI18n NameI18n { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionI18n")]
        public string DescriptionI18n { get; set; }

        [JsonProperty("order")]
        public long? Order { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("leaderUserId")]
        public string LeaderUserId { get; set; }
    }

    public class NameI18n
    {
        [JsonProperty("en")]
        public int En { get; set; }
    }
}
