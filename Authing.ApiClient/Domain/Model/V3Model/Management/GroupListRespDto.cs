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
    /// GroupListRespDto 的模型
    /// </summary>
    public partial class GroupListRespDto
    {
        /// <summary>
        ///  业务状态码，可以通过此状态码判断操作是否成功，200 表示成功。
        /// </summary>
        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }
        /// <summary>
        ///  描述信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        ///  细分错误码，可通过此错误码得到具体的错误类型。
        /// </summary>
        [JsonProperty("apiCode")]
        public long ApiCode { get; set; }
        /// <summary>
        ///  响应数据
        /// </summary>
        [JsonProperty("data")]
        public List<GroupDto> Data { get; set; }
    }
}