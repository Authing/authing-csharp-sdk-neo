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
    /// UserLoginHistoryPagingDto 的模型
    /// </summary>
    public partial class UserLoginHistoryPagingDto
    {
        /// <summary>
        ///  记录总数
        /// </summary>
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
        /// <summary>
        ///  响应数据
        /// </summary>
        [JsonProperty("list")]
        public List<UserLoginHistoryDto> List { get; set; }
    }
}