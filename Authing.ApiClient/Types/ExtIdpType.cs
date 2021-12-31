using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public enum ExtIdpType
    {
        [JsonProperty("oidc")]
        [Description("oidc")]
        OIDC,

        [JsonProperty("oauth2")]
        [Description("oauth2")]
        OAUTH,

        [JsonProperty("saml")]
        [Description("saml")]
        SAML,

        [JsonProperty("ldap")]
        [Description("ldap")]
        LDAP,

        [JsonProperty("ad")]
        [Description("ad")]
        AD,

        [JsonProperty("cas")]
        [Description("cas")]
        CAS,

        [JsonProperty("azure-ad")]
        [Description("azure-ad")]
        AZURE_AD,

        [JsonProperty("wechat")]
        [Description("wechat")]
        WECHAT,

        [JsonProperty("google")]
        [Description("google")]
        GOOGLE,

        [JsonProperty("qq")]
        [Description("qq")]
        QQ,

        [JsonProperty("wechatwork")]
        [Description("wechatwork")]
        WECHAT_WORK,

        [JsonProperty("dingtalk")]
        [Description("dingtalk")]
        DINGTALK,

        [JsonProperty("weibo")]
        [Description("weibo")]
        WEIBO,

        [JsonProperty("github")]
        [Description("github")]
        GITHUB,

        [JsonProperty("alipay")]
        [Description("alipay")]
        ALIPAY,

        [JsonProperty("apple")]
        [Description("apple")]
        APPLE,

        [JsonProperty("baidu")]
        [Description("baidu")]
        BAIDU,

        [JsonProperty("lark")]
        [Description("lark")]
        LARK,

        [JsonProperty("gitlab")]
        [Description("gitlab")]
        GITLAB,

        [JsonProperty("twitter")]
        [Description("twitter")]
        TWITTER,

        [JsonProperty("facebook")]
        [Description("facebook")]
        FACEBOOK,

        [JsonProperty("slack")]
        [Description("slack")]
        SLACK,

        [JsonProperty("linkedin")]
        [Description("linkedin")]
        LINKEDIN,
    }

    public enum ExtIdpConnType
    {
        [JsonProperty("oidc")]
        [Description("oidc")]
        OIDC,

        [JsonProperty("oauth")]
        [Description("oauth")]
        OAUTH,

        [JsonProperty("saml")]
        [Description("saml")]
        SAML,

        [JsonProperty("ldap")]
        [Description("ldap")]
        LDAP,

        [JsonProperty("ad")]
        [Description("ad")]
        AD,

        [JsonProperty("cas")]
        [Description("cas")]
        CAS,

        [JsonProperty("azure-ad")]
        [Description("azure-ad")]
        AZURE_AD,

        [JsonProperty("alipay")]
        [Description("alipay")]
        ALIPAY,

        [JsonProperty("facebook")]
        [Description("facebook")]
        FACEBOOK,

        [JsonProperty("twitter")]
        [Description("twitter")]
        TWITTER,

        [JsonProperty("google")]
        [Description("google")]
        GOOGLE,

        [JsonProperty("wechat:pc")]
        [Description("wechat:pc")]
        WECHATPC, // 微信 PC 端网页扫码登录

        [JsonProperty("wechat:mobile")]
        [Description("wechat:mobile")]
        WECHATMOBILE, // 原生 APP 内部调用微信登录

        [JsonProperty("wechat:webpage-authorization")]
        [Description("wechat:webpage-authorization")]
        WECHAT_BROWSER_AUTHZ, // 微信浏览器内部网页授权登录

        [JsonProperty("wechatmp-qrcode")]
        [Description("wechatmp-qrcode")]
        WECHAT_OFFICIAL_ACCOUNT_SUBSCRIPTION_EVENT, // 接收微信公众号扫码、关注事件，自动创建用户

        [JsonProperty("wechat:miniprogram:default")]
        [Description("wechat:miniprogram:default")]
        WECHAT_MINIPROGRAM, // 用户自主开发小程序内部登录

        [JsonProperty("wechat:miniprogram:qrconnect")]
        [Description("wechat:miniprogram:qrconnect")]
        WECHAT_MINIPROGRAM_QRCODE, // 『Authing 小登录』扫码登录

        [JsonProperty("wechat:miniprogram:app-launch")]
        [Description("wechat:miniprogram:app-launch")]
        WECHAT_MINIPROGRAM_APPLAUNCH, // 原生 APP 拉起小登录

        [JsonProperty("github")]
        [Description("github")]
        GITHUB,

        [JsonProperty("qq")]
        [Description("qq")]
        QQ,

        [JsonProperty("wechatwork:corp:qrconnect")]
        [Description("wechatwork:corp:qrconnect")]
        WECHATWORK_CORP_QRCONNECT,

        [JsonProperty("wechatwork:service-provider:qrconnect")]
        [Description("wechatwork:service-provider:qrconnect")]
        WECHATWORK_SERVICEPROVIDER_QRCONNECT,

        [JsonProperty("dingtalk")]
        [Description("dingtalk")]
        DINGTALK,

        [JsonProperty("weibo")]
        [Description("weibo")]
        WEIBO,

        [JsonProperty("apple")]
        [Description("apple")]
        APPLE,

        [JsonProperty("apple:web")]
        [Description("apple:web")]
        APPLE_WEB,

        [JsonProperty("baidu")]
        [Description("baidu")]
        BAIDU,

        [JsonProperty("lark-internal")]
        [Description("lark-internal")]
        LARK_INTERNAL,

        [JsonProperty("lark-public")]
        [Description("lark-public")]
        LARK_PUBLIC,

        [JsonProperty("gitlab")]
        [Description("gitlab")]
        GITLAB,

        [JsonProperty("linkedin")]
        [Description("linkedin")]
        LINKEDIN,

        [JsonProperty("slack")]
        [Description("slack")]
        SLACK,
    }
}
