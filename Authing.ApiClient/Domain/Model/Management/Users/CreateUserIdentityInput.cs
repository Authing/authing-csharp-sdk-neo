using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class CreateUserIdentityInput
    {
        #region members
        [JsonProperty("provider")]
        [JsonRequired]
        public string Provider { get; set; }

        [JsonProperty("userIdInIdp")]
        [JsonRequired]
        public string UserIdInIdp { get; set; }

        [JsonProperty("openid")]
        public string Openid { get; set; }

        [JsonProperty("isSocial")]
        public bool? IsSocial { get; set; }

        [JsonProperty("connectionId")]
        public string ConnectionId { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
        #endregion


        /// <summary>
        /// <param name="provider">provider</param>
        /// <param name="userIdInIdp">userIdInIdp</param>
        /// </summary>

        public CreateUserIdentityInput(string provider, string userIdInIdp)
        {
            this.Provider = provider;
            this.UserIdInIdp = userIdInIdp;
        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }
}