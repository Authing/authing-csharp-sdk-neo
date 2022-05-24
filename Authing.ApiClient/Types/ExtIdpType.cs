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
}
