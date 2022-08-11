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
    /// GetDepartmentDto 的模型
    /// </summary>
    public partial class GetDepartmentDto
    {
        /// <summary>
        ///  组织 code
        /// </summary>
        [JsonProperty("organizationCode")]
        public object OrganizationCode { get; set; }
        /// <summary>
        ///  部门 ID，根部门传 `root`。departmentId 和 departmentCode 必传其一。
        /// </summary>
        [JsonProperty("departmentId")]
        public object DepartmentId { get; set; }
        /// <summary>
        ///  部门 code。departmentId 和 departmentCode 必传其一。
        /// </summary>
        [JsonProperty("departmentCode")]
        public object DepartmentCode { get; set; }
        /// <summary>
        ///  此次调用中使用的部门 ID 的类型
        /// </summary>
        [JsonProperty("departmentIdType")]
        public object DepartmentIdType { get; set; }
        /// <summary>
        ///  是否获取自定义数据
        /// </summary>
        [JsonProperty("withCustomData")]
        public object WithCustomData { get; set; }
    }
}