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
    /// IsUserInDepartmentDto 的模型
    /// </summary>
    public partial class IsUserInDepartmentDto
    {
        /// <summary>
        ///  用户 ID
        /// </summary>
        [JsonProperty("userId")]
        public object UserId { get; set; }
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
        ///  此次调用中使用的部门 ID 的类型
        /// </summary>
        [JsonProperty("departmentIdType")]
        public object DepartmentIdType { get; set; }
        /// <summary>
        ///  是否包含子部门
        /// </summary>
        [JsonProperty("includeChildrenDepartments")]
        public object IncludeChildrenDepartments { get; set; }
    }
}