using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.Authentication
{
    public class QrCodeCheckStatusResponse
    {
        [JsonProperty("random")]
        public string Random { get; set; }

        /// <summary>
        ///二维码状态
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <summary>
        /// 用于换取用户信息的一个随机字符串
        /// </summary>
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
    }

    public enum Status
    {
        /// <summary>
        /// 未使用
        /// </summary>
        NotUse = 0,
        /// <summary>
        /// 已扫码
        /// </summary>
        Scanned = 1,
        /// <summary>
        /// 已授权
        /// </summary>
        Authorized,
        /// <summary>
        /// 取消授权
        /// </summary>
        CancelAuthorized,
        /// <summary>
        /// 已过期
        /// </summary>
        TimeOut = -1
    }

}
