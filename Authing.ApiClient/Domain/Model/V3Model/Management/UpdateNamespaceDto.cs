using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Authing.Library.Domain.Model.V3Model.Management.Models
{
    /// <summary>
    /// UpdateNamespaceDto 的模型
    /// </summary>
    public partial class UpdateNamespaceDto
    {
        /// <summary>
        ///  权限分组唯一标志符
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        ///  权限分组描述信息
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        ///  权限分组名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        ///  权限分组新的唯一标志符
        /// </summary>
        [JsonProperty("newCode")]
        public string NewCode { get; set; }
    }
}