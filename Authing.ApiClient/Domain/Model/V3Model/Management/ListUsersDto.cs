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
    /// ListUsersDto 的模型
    /// </summary>
    public partial class ListUsersDto
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
        ///  账户当前状态，如 已停用、已离职、正常状态、已归档
        /// </summary>
        [JsonProperty("status")]
        public object Status { get; set; }
        /// <summary>
        ///  用户创建、修改开始时间，为精确到秒的 UNIX 时间戳；支持获取从某一段时间之后的增量数据
        /// </summary>
        [JsonProperty("updatedAtStart")]
        public object UpdatedAtStart { get; set; }
        /// <summary>
        ///  用户创建、修改终止时间，为精确到秒的 UNIX 时间戳；支持获取某一段时间内的增量数据。默认为当前时间
        /// </summary>
        [JsonProperty("updatedAtEnd")]
        public object UpdatedAtEnd { get; set; }
        /// <summary>
        ///  是否获取自定义数据
        /// </summary>
        [JsonProperty("withCustomData")]
        public object WithCustomData { get; set; }
        /// <summary>
        ///  是否获取 identities
        /// </summary>
        [JsonProperty("withIdentities")]
        public object WithIdentities { get; set; }
        /// <summary>
        ///  是否获取部门 ID 列表
        /// </summary>
        [JsonProperty("withDepartmentIds")]
        public object WithDepartmentIds { get; set; }
    }
}