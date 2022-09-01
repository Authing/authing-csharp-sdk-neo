using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.Authentication
{
    public class ForceLoginResponse
    {
        /// <summary>
        /// 强制更新密码需要传的 Token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("nickname")]
        public object Nickname { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }

}
