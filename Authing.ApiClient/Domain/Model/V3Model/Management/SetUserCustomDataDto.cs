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
    /// SetUserCustomDataDto 的模型
    /// </summary>
    public partial class SetUserCustomDataDto
    {
        /// <summary>
        ///  操作是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}