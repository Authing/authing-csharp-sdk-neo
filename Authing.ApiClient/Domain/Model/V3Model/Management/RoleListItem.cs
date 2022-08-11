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
    /// RoleListItem 的模型
    /// </summary>
    public partial class RoleListItem
    {
        /// <summary>
        ///  权限分组内角色的唯一标识符
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        ///  角色描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        ///  所属权限分组的 code
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}