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
    /// UserMfaRespDto 的模型
    /// </summary>
    public partial class UserMfaRespDto
    {
        /// <summary>
        ///  是否绑定了 TOTP，可选值 enabled, disabled
        /// </summary>
        [JsonProperty("totpStatus")]
        public string TotpStatus { get; set; }
        /// <summary>
        ///  是否绑定了人脸 MFA，可选值 enabled, disabled
        /// </summary>
        [JsonProperty("faceMfaStatus")]
        public string FaceMfaStatus { get; set; }
    }
}