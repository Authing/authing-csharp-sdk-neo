﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    #region CreateUserInput
    public class CreateUserInput
    {
        #region members
        /// <summary>
        /// 用户名，用户池内唯一
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// 邮箱，不区分大小写，如 Bob@example.com 和 bob@example.com 会识别为同一个邮箱。用户池内唯一。
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 邮箱是否已验证
        /// </summary>
        [JsonProperty("emailVerified")]
        public bool? EmailVerified { get; set; } = false;

        /// <summary>
        /// 手机号，用户池内唯一
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 手机号是否已验证
        /// </summary>
        [JsonProperty("phoneVerified")]
        public bool? PhoneVerified { get; set; } = false;

        [JsonProperty("unionid")]
        public string Unionid { get; set; }

        [JsonProperty("openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 昵称，该字段不唯一。
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像链接，默认为 https://usercontents.authing.cn/authing-avatar.png
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; set; } = "https://usercontents.authing.cn/authing-avatar.png";

        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// 注册方式
        /// </summary>
        [JsonProperty("registerSource")]
        public IEnumerable<string> RegisterSource { get; set; }

        [JsonProperty("browser")]
        public string Browser { get; set; }

        /// <summary>
        /// 用户社会化登录第三方身份提供商返回的原始用户信息，非社会化登录方式注册的用户此字段为空。
        /// </summary>
        [JsonProperty("oauth")]
        public string Oauth { get; set; }

        /// <summary>
        /// 用户累计登录次数，当你从你原有用户系统向 Authing 迁移的时候可以设置此字段。
        /// </summary>
        [JsonProperty("loginsCount")]
        public int? LoginsCount { get; set; } = 0;

        [JsonProperty("lastLogin")]
        public string LastLogin { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("lastIP")]
        public string LastIp { get; set; }

        /// <summary>
        /// 用户注册时间，当你从你原有用户系统向 Authing 迁移的时候可以设置此字段。
        /// </summary>
        [JsonProperty("signedUp")]
        public string SignedUp { get; set; }

        [JsonProperty("blocked")]
        public bool? Blocked { get; set; }

        [JsonProperty("isDeleted")]
        public bool? IsDeleted { get; set; } = false;

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("preferredUsername")]
        public string PreferredUsername { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("zoneinfo")]
        public string Zoneinfo { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }
        #endregion



        public CreateUserInput()
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
    #endregion

    #region CreateUserIdentityInput

    #endregion

    #region CreateUsersRes

    #endregion

    #region CreateUserResult

    #endregion
}
