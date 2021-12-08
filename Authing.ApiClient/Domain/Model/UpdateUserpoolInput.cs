using System;
using System.Collections.Generic;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UpdateUserpoolInput
    {
        #region members
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("userpoolTypes")]
        public IEnumerable<string> UserpoolTypes { get; set; }

        [JsonProperty("emailVerifiedDefault")]
        public bool? EmailVerifiedDefault { get; set; }

        [JsonProperty("sendWelcomeEmail")]
        public bool? SendWelcomeEmail { get; set; }

        [JsonProperty("registerDisabled")]
        public bool? RegisterDisabled { get; set; }

        /// <summary>
        /// @deprecated
        /// </summary>
        [JsonProperty("appSsoEnabled")]
        public bool? AppSsoEnabled { get; set; }

        [JsonProperty("allowedOrigins")]
        public string AllowedOrigins { get; set; }

        [JsonProperty("tokenExpiresAfter")]
        public int? TokenExpiresAfter { get; set; }

        [JsonProperty("frequentRegisterCheck")]
        public FrequentRegisterCheckConfigInput FrequentRegisterCheck { get; set; }

        [JsonProperty("loginFailCheck")]
        public LoginFailCheckConfigInput LoginFailCheck { get; set; }

        /// <summary>
        /// 密码重置策略
        /// </summary>
        [JsonProperty("passwordUpdatePolicy")]
        public PasswordUpdatePolicyInput PasswordUpdatePolicy { get; set; }

        [JsonProperty("loginFailStrategy")]
        public string LoginFailStrategy { get; set; }

        [JsonProperty("loginPasswordFailCheck")]
        public LoginPasswordFailCheckConfigInput LoginPasswordFailCheck { get; set; }

        [JsonProperty("changePhoneStrategy")]
        public ChangePhoneStrategyInput ChangePhoneStrategy { get; set; }

        [JsonProperty("changeEmailStrategy")]
        public ChangeEmailStrategyInput ChangeEmailStrategy { get; set; }

        [JsonProperty("qrcodeLoginStrategy")]
        public QrcodeLoginStrategyInput QrcodeLoginStrategy { get; set; }

        [JsonProperty("app2WxappLoginStrategy")]
        public App2WxappLoginStrategyInput App2WxappLoginStrategy { get; set; }

        [JsonProperty("whitelist")]
        public RegisterWhiteListConfigInput Whitelist { get; set; }

        /// <summary>
        /// 自定义短信服务商配置
        /// </summary>
        [JsonProperty("customSMSProvider")]
        public CustomSmsProviderInput CustomSmsProvider { get; set; }

        /// <summary>
        /// 是否要求邮箱必须验证才能登录（如果是通过邮箱登录的话）
        /// </summary>
        [JsonProperty("loginRequireEmailVerified")]
        public bool? LoginRequireEmailVerified { get; set; }

        [JsonProperty("verifyCodeLength")]
        public int? VerifyCodeLength { get; set; }
        #endregion

        public UpdateUserpoolInput()
        {

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