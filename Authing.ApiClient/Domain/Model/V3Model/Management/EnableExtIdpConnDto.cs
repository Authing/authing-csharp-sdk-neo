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
    /// EnableExtIdpConnDto 的模型
    /// </summary>
    public partial class EnableExtIdpConnDto
    {
        /// <summary>
        ///  应用 ID
        /// </summary>
        [JsonProperty("appId")]
        public string AppId { get; set; }
        /// <summary>
        ///  是否开启身份源连接
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary>
        ///  身份源连接 ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        ///  租户 ID
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
    }
}