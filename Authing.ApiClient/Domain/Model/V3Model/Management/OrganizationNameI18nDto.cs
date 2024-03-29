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
    /// OrganizationNameI18nDto 的模型
    /// </summary>
    public partial class OrganizationNameI18nDto
    {
        /// <summary>
        ///  支持多语言的字段
        /// </summary>
        [JsonProperty("organizationName")]
        public LangObject OrganizationName { get; set; }
    }
}