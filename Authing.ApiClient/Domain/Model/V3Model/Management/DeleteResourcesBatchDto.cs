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
    /// DeleteResourcesBatchDto 的模型
    /// </summary>
    public partial class DeleteResourcesBatchDto
    {
        /// <summary>
        ///  资源 code 列表
        /// </summary>
        [JsonProperty("codeList")]
        public List<string> CodeList { get; set; }
        /// <summary>
        ///  所属权限分组的 code
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}