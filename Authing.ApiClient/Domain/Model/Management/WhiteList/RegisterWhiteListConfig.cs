using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.WhiteList
{
    public class RegisterWhiteListConfig
    {
        #region members
        /// <summary>
        /// 是否开启手机号注册白名单
        /// </summary>
        [JsonProperty("phoneEnabled")]
        public bool? PhoneEnabled { get; set; }

        /// <summary>
        /// 是否开启邮箱注册白名单
        /// </summary>
        [JsonProperty("emailEnabled")]
        public bool? EmailEnabled { get; set; }

        /// <summary>
        /// 是否开用户名注册白名单
        /// </summary>
        [JsonProperty("usernameEnabled")]
        public bool? UsernameEnabled { get; set; }
        #endregion
    }
}
