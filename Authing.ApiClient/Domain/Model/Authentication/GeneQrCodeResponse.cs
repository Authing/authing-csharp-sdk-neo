using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.Authentication
{
    /// <summary>
    /// 生成二维码响应数据
    /// </summary>
    public class GeneQrCodeResponse
    {
        /// <summary>
        /// 二维码唯一 ID
        /// </summary>
        [JsonProperty("random")]
        public string Random { get; set; }
        /// <summary>
        /// 二维码链接
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
