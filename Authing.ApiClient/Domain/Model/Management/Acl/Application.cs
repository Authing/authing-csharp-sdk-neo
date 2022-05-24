using System;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Application
    {
        public Qrcodescanning qrcodeScanning { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string userPoolId { get; set; }
        public string protocol { get; set; }
        public bool isOfficial { get; set; }
        public bool isDeleted { get; set; }
        public bool isDefault { get; set; }
        public bool isDemo { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public string identifier { get; set; }
        public KeyValuePair<string,string> jwks { get; set; }
        public object ssoPageCustomizationSettings { get; set; }
        public string logo { get; set; }
        public IEnumerable<string> redirectUris { get; set; }
        public IEnumerable<string> logoutRedirectUris { get; set; }
        public object initLoginUrl { get; set; }
        public bool oidcProviderEnabled { get; set; }
        public bool oauthProviderEnabled { get; set; }
        public bool samlProviderEnabled { get; set; }
        public bool casProviderEnabled { get; set; }
        public bool registerDisabled { get; set; }
        public string[] loginTabs { get; set; }
        public Passwordtabconfig passwordTabConfig { get; set; }
        public string defaultLoginTab { get; set; }
        public IEnumerable<string> registerTabs { get; set; }
        public string defaultRegisterTab { get; set; }
        public bool extendsFieldsEnabled { get; set; }
        public object[] extendsFields { get; set; }
        public object[] complateFiledsPlace { get; set; }
        public bool skipComplateFileds { get; set; }
        public object ext { get; set; }
        public string css { get; set; }
        public Oidcconfig oidcConfig { get; set; }
        public object oidcJWEConfig { get; set; }
        public object samlConfig { get; set; }
        public Oauthconfig oauthConfig { get; set; }
        public object casConfig { get; set; }
        public bool showAuthorizationPage { get; set; }
        public bool enableSubAccount { get; set; }
        public bool enableDeviceMutualExclusion { get; set; }
        public bool loginRequireEmailVerified { get; set; }
        public bool agreementEnabled { get; set; }
        public bool isIntegrate { get; set; }
        public bool ssoEnabled { get; set; }
        public object template { get; set; }
        public bool skipMfa { get; set; }
        public bool casExpireBaseBrowser { get; set; }
        public string appType { get; set; }
        public Permissionstrategy permissionStrategy { get; set; }
    }
}