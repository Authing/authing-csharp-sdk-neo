using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model
{
    public class CommonMessage
    {
        #region members
        /// <summary>
        /// 可读的接口响应说明，请以业务状态码 code 作为判断业务是否成功的标志
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 业务状态码（与 HTTP 响应码不同），但且仅当为 200 的时候表示操作成功表示，详细说明请见：
        /// [Authing 错误代码列表](https://docs.authing.co/advanced/error-code.html)
        /// </summary>
        [JsonProperty("code")]
        public int? Code { get; set; }
        #endregion
    }
}
