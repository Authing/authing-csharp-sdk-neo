using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.V3Model.Management.Group
{
    /// <summary>
    /// GroupDto 的模型
    /// </summary>
    public partial class GroupDto
    {
        /// <summary>
        ///  分组 code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        ///  分组名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        ///  分组描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
