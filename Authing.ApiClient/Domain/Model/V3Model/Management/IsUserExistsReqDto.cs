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
    /// IsUserExistsReqDto 的模型
    /// </summary>
    public partial class IsUserExistsReqDto
    {
        /// <summary>
        ///  用户名，用户池内唯一
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        /// <summary>
        ///  邮箱
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        ///  手机号
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
        /// <summary>
        ///  第三方外部 ID
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }
    }
}