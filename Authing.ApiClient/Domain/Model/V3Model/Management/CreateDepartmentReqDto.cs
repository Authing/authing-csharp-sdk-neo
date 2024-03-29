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
    /// CreateDepartmentReqDto 的模型
    /// </summary>
    public partial class CreateDepartmentReqDto
    {
        /// <summary>
        ///  父部门 id
        /// </summary>
        [JsonProperty("parentDepartmentId")]
        public string ParentDepartmentId { get; set; }
        /// <summary>
        ///  部门名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        ///  组织 Code（organizationCode）
        /// </summary>
        [JsonProperty("organizationCode")]
        public string OrganizationCode { get; set; }
        /// <summary>
        ///  自定义部门 ID，用于存储自定义的 ID
        /// </summary>
        [JsonProperty("openDepartmentId")]
        public string OpenDepartmentId { get; set; }
        /// <summary>
        ///  部门描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        ///  部门识别码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        ///  是否是虚拟部门
        /// </summary>
        [JsonProperty("isVirtualNode")]
        public bool IsVirtualNode { get; set; }
        /// <summary>
        ///  多语言设置
        /// </summary>
        [JsonProperty("i18n")]
        public I18nDto I18n { get; set; }
        /// <summary>
        ///  部门的扩展字段数据
        /// </summary>
        [JsonProperty("customData")]
        public object CustomData { get; set; }
        /// <summary>
        ///  此次调用中使用的父部门 ID 的类型
        /// </summary>
        [JsonProperty("departmentIdType")]
        public departmentIdType DepartmentIdType { get; set; }
    }
    public partial class CreateDepartmentReqDto
    {
        /// <summary>
        ///  此次调用中使用的父部门 ID 的类型
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