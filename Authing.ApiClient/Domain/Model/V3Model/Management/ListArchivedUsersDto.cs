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
    /// ListArchivedUsersDto 的模型
    /// </summary>
    public partial class ListArchivedUsersDto
    {
        /// <summary>
        ///  当前页数，从 1 开始
        /// </summary>
        [JsonProperty("page")]
        public object Page { get; set; }
        /// <summary>
        ///  每页数目，最大不能超过 50，默认为 10
        /// </summary>
        [JsonProperty("limit")]
        public object Limit { get; set; }
        /// <summary>
        ///  开始时间，为精确到秒的 UNIX 时间戳，默认不指定
        /// </summary>
        [JsonProperty("startAt")]
        public object StartAt { get; set; }
    }
}