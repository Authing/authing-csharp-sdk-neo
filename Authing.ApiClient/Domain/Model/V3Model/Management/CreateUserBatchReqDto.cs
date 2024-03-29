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
    /// CreateUserBatchReqDto 的模型
    /// </summary>
    public partial class CreateUserBatchReqDto
    {
        /// <summary>
        ///  用户列表
        /// </summary>
        [JsonProperty("list")]
        public List<CreateUserInfoDto> List { get; set; }
        /// <summary>
        ///  可选参数
        /// </summary>
        [JsonProperty("options")]
        public CreateUserOptionsDto Options { get; set; }
    }
}