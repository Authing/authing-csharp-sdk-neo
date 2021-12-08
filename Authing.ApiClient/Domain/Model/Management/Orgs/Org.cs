using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class Org
    {
        #region members
        /// <summary>
        /// 组织机构 ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 根节点
        /// </summary>
        [JsonProperty("rootNode")]
        public Node RootNode { get; set; }

        /// <summary>
        /// 组织机构节点列表
        /// </summary>
        [JsonProperty("nodes")]
        public IEnumerable<Node> Nodes { get; set; }
        #endregion
    }
}
