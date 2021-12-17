using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class RegisterByUsernameInput
    {
        #region members
        [JsonProperty("username")]
        [JsonRequired]
        public string Username { get; set; }

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
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// </summary>

        public RegisterByUsernameInput(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        #region methods
        public dynamic GetInputObject()
        {
            //IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            //var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            //foreach (var propertyInfo in properties)
            //{
            //    var value = propertyInfo.GetValue(this);
            //    var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

            //    var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
            //    if (requiredProp || value != defaultValue)
            //    {
            //        d[propertyInfo.Name] = value;
            //    }
            //}
            //return d;
            return ReflectionHelper.GetInputObjec(this);
        }
        #endregion
    }
}
