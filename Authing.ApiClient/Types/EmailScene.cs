using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    /// <summary>
    /// 邮件使用场景
    /// </summary>
    public enum EmailScene
    {
        /// <summary>
        /// 发送重置密码邮件，邮件中包含验证码
        /// </summary>
        [JsonProperty("RESET_PASSWORD")]
        RESET_PASSWORD,
        /// <summary>
        /// 发送验证邮箱的邮件
        /// </summary>
        [JsonProperty("VERIFY_EMAIL")]
        VERIFY_EMAIL,
        /// <summary>
        /// 发送修改邮箱邮件，邮件中包含验证码
        /// </summary>
        [JsonProperty("CHANGE_EMAIL")]
        CHANGE_EMAIL,
        /// <summary>
        /// 发送 MFA 验证邮件
        /// </summary>
        [JsonProperty("MFA_VERIFY")]
        MFA_VERIFY
    }
}
