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
    /// IsActionAllowedDataDto 的模型
    /// </summary>
    public partial class IsActionAllowedDataDto
    {
        /// <summary>
        ///  是否允许
        /// </summary>
        [JsonProperty("allowed")]
        public bool Allowed { get; set; }
    }
}