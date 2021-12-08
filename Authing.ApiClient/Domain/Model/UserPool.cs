using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UserPool
    {
        #region members
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("jwtSecret")]
        public string JwtSecret { get; set; }

        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty("userpoolTypes")]
        public IEnumerable<UserPoolType> UserpoolTypes { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// 用户邮箱是否验证（用户的 emailVerified 字段）默认值，默认为 false
        /// </summary>
        [JsonProperty("emailVerifiedDefault")]
        public bool EmailVerifiedDefault { get; set; }

        /// <summary>
        /// 用户注册之后是否发送欢迎邮件
        /// </summary>
        [JsonProperty("sendWelcomeEmail")]
        public bool SendWelcomeEmail { get; set; }

        /// <summary>
        /// 是否关闭注册
        /// </summary>
        [JsonProperty("registerDisabled")]
        public bool RegisterDisabled { get; set; }

        /// <summary>
        /// @deprecated 是否开启用户池下应用间单点登录
        /// </summary>
        [JsonProperty("appSsoEnabled")]
        public bool AppSsoEnabled { get; set; }

        /// <summary>
        /// 用户池禁止注册后，是否还显示微信小程序扫码登录。当 **showWXMPQRCode** 为 **true** 时，
        /// 前端显示小程序码，此时只有以前允许注册时，扫码登录过的用户可以继续登录；新用户扫码无法登录。
        /// </summary>
        [JsonProperty("showWxQRCodeWhenRegisterDisabled")]
        public bool? ShowWxQrCodeWhenRegisterDisabled { get; set; }

        /// <summary>
        /// 前端跨域请求白名单
        /// </summary>
        [JsonProperty("allowedOrigins")]
        public string AllowedOrigins { get; set; }

        /// <summary>
        /// 用户 **token** 有效时间，单位为秒，默认为 15 天。
        /// </summary>
        [JsonProperty("tokenExpiresAfter")]
        public int? TokenExpiresAfter { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        [JsonProperty("isDeleted")]
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 注册频繁检测
        /// </summary>
        [JsonProperty("frequentRegisterCheck")]
        public FrequentRegisterCheckConfig FrequentRegisterCheck { get; set; }

        /// <summary>
        /// 登录失败检测
        /// </summary>
        [JsonProperty("loginFailCheck")]
        public LoginFailCheckConfig LoginFailCheck { get; set; }

        /// <summary>
        /// 密码重置策略
        /// </summary>
        [JsonProperty("passwordUpdatePolicy")]
        public PasswordUpdatePolicyConfig PasswordUpdatePolicy { get; set; }

        /// <summary>
        /// 登录失败检测
        /// </summary>
        [JsonProperty("loginPasswordFailCheck")]
        public LoginPasswordFailCheckConfig LoginPasswordFailCheck { get; set; }

        /// <summary>
        /// 密码安全策略
        /// </summary>
        [JsonProperty("loginFailStrategy")]
        public string LoginFailStrategy { get; set; }

        /// <summary>
        /// 手机号修改策略
        /// </summary>
        [JsonProperty("changePhoneStrategy")]
        public ChangePhoneStrategy ChangePhoneStrategy { get; set; }

        /// <summary>
        /// 邮箱修改策略
        /// </summary>
        [JsonProperty("changeEmailStrategy")]
        public ChangeEmailStrategy ChangeEmailStrategy { get; set; }

        /// <summary>
        /// APP 扫码登录配置
        /// </summary>
        [JsonProperty("qrcodeLoginStrategy")]
        public QrcodeLoginStrategy QrcodeLoginStrategy { get; set; }

        /// <summary>
        /// APP 拉起小程序登录配置
        /// </summary>
        [JsonProperty("app2WxappLoginStrategy")]
        public App2WxappLoginStrategy App2WxappLoginStrategy { get; set; }

        /// <summary>
        /// 注册白名单配置
        /// </summary>
        [JsonProperty("whitelist")]
        public RegisterWhiteListConfig Whitelist { get; set; }

        /// <summary>
        /// 自定义短信服务商配置
        /// </summary>
        [JsonProperty("customSMSProvider")]
        public CustomSMSProvider CustomSmsProvider { get; set; }

        /// <summary>
        /// 用户池套餐类型
        /// </summary>
        [JsonProperty("packageType")]
        public int? PackageType { get; set; }

        /// <summary>
        /// 是否使用自定义数据库 CUSTOM_USER_STORE 模式
        /// </summary>
        [JsonProperty("useCustomUserStore")]
        public bool? UseCustomUserStore { get; set; }

        /// <summary>
        /// 是否要求邮箱必须验证才能登录（如果是通过邮箱登录的话）
        /// </summary>
        [JsonProperty("loginRequireEmailVerified")]
        public bool? LoginRequireEmailVerified { get; set; }

        /// <summary>
        /// 短信验证码长度
        /// </summary>
        [JsonProperty("verifyCodeLength")]
        public int? VerifyCodeLength { get; set; }
        #endregion
    }
}
