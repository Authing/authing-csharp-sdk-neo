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
    /// HasAnyRoleDto 的模型
    /// </summary>
    public partial class HasAnyRoleDto
    {
        /// <summary>
        ///  是否拥有其中某一个角色
        /// </summary>
        [JsonProperty("hasAnyRole")]
        public bool HasAnyRole { get; set; }
    }
}