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
    /// ExtIdpDto 的模型
    /// </summary>
    public partial class ExtIdpDto
    {
        /// <summary>
        ///  身份源 id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        ///  身份源名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        ///  租户 ID
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
        /// <summary>
        ///  身份源类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}