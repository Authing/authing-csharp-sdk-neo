using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class RegisterByEmailInput
    {
        #region members
        [JsonProperty("email")]
        [JsonRequired]
        public string Email { get; set; }

        [JsonProperty("password")]
        [JsonRequired]
        public string Password { get; set; }

        [JsonProperty("profile")]
        public RegisterProfile Profile { get; set; }

        [JsonProperty("forceLogin")]
        public bool? ForceLogin { get; set; }

        [JsonProperty("generateToken")]
        public bool? GenerateToken { get; set; }

        [JsonProperty("clientIp")]
        public string ClientIp { get; set; }

        /// <summary>
        /// 设置用户自定义字段，要求符合 Array<{ key: string; value: string }> 格式
        /// </summary>
        [JsonProperty("params")]
        public string Params { get; set; }

        /// <summary>
        /// 请求上下文信息，将会传递到 pipeline 中
        /// </summary>
        [JsonProperty("context")]
        public string Context { get; set; }
        #endregion


        /// <summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// </summary>

        public RegisterByEmailInput(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        #region methods
        public dynamic GetInputObject()
        {
            return ReflectionHelper.GetInputObjec(this);
        }
        #endregion
    }
}
