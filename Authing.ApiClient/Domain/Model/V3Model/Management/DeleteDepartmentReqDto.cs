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
    /// DeleteDepartmentReqDto 的模型
    /// </summary>
    public partial class DeleteDepartmentReqDto
    {
        /// <summary>
        ///  部门系统 ID（为 Authing 系统自动生成，不可修改）
        /// </summary>
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }
        /// <summary>
        ///  组织 Code（organizationCode）
        /// </summary>
        [JsonProperty("organizationCode")]
        public string OrganizationCode { get; set; }
        /// <summary>
        ///  此次调用中使用的部门 ID 的类型
        /// </summary>
        [JsonProperty("departmentIdType")]
        public departmentIdType DepartmentIdType { get; set; }
    }
    public partial class DeleteDepartmentReqDto
    {
        /// <summary>
        ///  此次调用中使用的部门 ID 的类型
        /// </summary>
        public enum departmentIdType
        {
            [EnumMember(Value = "department_id")]
            DEPARTMENT_ID,
            [EnumMember(Value = "open_department_id")]
            OPEN_DEPARTMENT_ID,
        }
    }
}