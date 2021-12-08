using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum ProviderTypeEnum
    {
        /// <summary>
        /// 钉钉
        /// </summary>
        [JsonProperty("DINGTALK")]
        DINGTALK,
        /// <summary>
        /// 企业微信
        /// </summary>
        [JsonProperty("WECHATWORK")]
        WECHATWORK,
        /// <summary>
        /// AD
        /// </summary>
        [JsonProperty("AD")]
        AD,

    }
}
