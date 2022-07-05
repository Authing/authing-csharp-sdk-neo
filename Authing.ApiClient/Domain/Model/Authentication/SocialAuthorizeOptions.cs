
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Model.Authentication
{
    public class SocialAuthorizeOptions
    {
        /// <summary>
        /// 是否通过弹窗的方式打开社会化登录窗口，如果设置为 false，将会以 window.open 的方式打开一个新的浏览器  tab 。
        /// </summary>
        public bool Popup { get; set; }

        /// <summary>
        /// 用户同意授权事件回调函数，第一个参数为用户信息。
        /// </summary>
        public Action<object> OnSuccess { get; set; }

        /// <summary>
        /// 社会化登录失败事件回调函数，第一个参数 code 为错误码，第二个参数 message 为错误提示。详细的错误码列表请见：详细说明请见：[Authing 错误代码列表](https://docs.authing.co/advanced/error-code.html)
        /// </summary>
        public Action<int,string> OnError { get; set; }

        /// <summary>
        /// 只有当 options.popup 为 ture 的时候有效，弹出窗口的位置，默认为 { w: 585, h: 649 } 。
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// 请求时的额外参数
        /// </summary>
        [JsonProperty("authorizationParams")]
        public object AuthorizationParams { get; set; }

        /// <summary>
        /// 请求时的额外参数,兼容之前的代码
        /// </summary>
        [JsonProperty("authorization_params")]
        public object Authiorization_Params { get; set; }

        /// <summary>
        /// 上下文数据
        /// </summary>
        [JsonProperty("context")]
        public object Context { get; set; }

        [JsonProperty("customData")]
        public object CustomData { get; set; }

        /// <summary>
        /// 获取的用户信息中是否包含 identities
        /// </summary>
        [JsonProperty("withIdentities")]
        public bool WithIdentities { get; set; }

        /// <summary>
        /// 是否获取用户自定义数据
        /// </summary>
        [JsonProperty("withCustomData")]
        public bool WithCustomData { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("uuid")]
        public string UUID { get; set; }

        /// <summary>
        /// 租户 ID
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
    }

    public class Position
    { 
        public double X { get; set; }
        public double Y { get; set; }
    }
}
