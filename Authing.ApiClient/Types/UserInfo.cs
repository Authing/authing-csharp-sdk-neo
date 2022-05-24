using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public class UserInfo
    {
        [JsonProperty("sub")] public string Sub { get; set; }

        /// <summary>
        /// 用户池 ID
        /// </summary>
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        /// <summary>
        /// 用户名，用户池内唯一
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// 邮箱，用户池内唯一
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 邮箱是否已验证
        /// </summary>
        [JsonProperty("email_verified")]
        public bool? EmailVerified { get; set; }

        /// <summary>
        /// 手机号，用户池内唯一
        /// </summary>
        [JsonProperty("phone_number")]
        public string Phone { get; set; }

        /// <summary>
        /// 手机号是否已验证
        /// </summary>
        [JsonProperty("phone_number_verified")]
        public bool? PhoneVerified { get; set; }

        [JsonProperty("unionid")] public string Unionid { get; set; }

        /// <summary>
        /// 昵称，该字段不唯一。
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像链接，默认为 https://usercontents.authing.cn/authing-avatar.png
        /// </summary>
        [JsonProperty("picture")]
        public string Photo { get; set; }

        [JsonProperty("device")] public string Device { get; set; }

        [JsonProperty("browser")] public string Browser { get; set; }

        [JsonProperty("company")] public string Company { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("givenName")] public string GivenName { get; set; }

        [JsonProperty("familyName")] public string FamilyName { get; set; }

        [JsonProperty("middleName")] public string MiddleName { get; set; }

        [JsonProperty("profile")] public string Profile { get; set; }

        [JsonProperty("preferred_username")] public string PreferredUsername { get; set; }

        [JsonProperty("website")] public string Website { get; set; }

        [JsonProperty("gender")] public string Gender { get; set; }

        [JsonProperty("birthdate")] public string Birthdate { get; set; }

        [JsonProperty("zoneinfo")] public string Zoneinfo { get; set; }

        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("address")] public Address Address { get; set; }

        [JsonProperty("formatted")] public string Formatted { get; set; }

        [JsonProperty("streetAddress")] public string StreetAddress { get; set; }

        [JsonProperty("locality")] public string Locality { get; set; }

        [JsonProperty("region")] public string Region { get; set; }

        [JsonProperty("postalCode")] public string PostalCode { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("province")] public string Province { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("createdAt")] public string CreatedAt { get; set; }

        [JsonProperty("updated_at")] public string UpdatedAt { get; set; }

        /// 用户外部 ID
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("token")] public string Token { get; set; }
    }
}