using System;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Action
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }


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

    public class Qrcodescanning
    {
        public bool redirect { get; set; }
        public int interval { get; set; }
    }

    public class Passwordtabconfig
    {
        public string[] enabledLoginMethods { get; set; }
    }

    public class Oidcconfig
    {
        public IEnumerable<string> grant_types { get; set; }
        public IEnumerable<string> response_types { get; set; }
        public string id_token_signed_response_alg { get; set; }
        public string token_endpoint_auth_method { get; set; }
        public int authorization_code_expire { get; set; }
        public int id_token_expire { get; set; }
        public int access_token_expire { get; set; }
        public int refresh_token_expire { get; set; }
        public int cas_expire { get; set; }
        public bool skip_consent { get; set; }
        public IEnumerable<string> redirect_uris { get; set; }
        public object[] post_logout_redirect_uris { get; set; }
        public string client_id { get; set; }
    }

    public class Oauthconfig
    {
        public string id { get; set; }
        public IEnumerable<string> redirect_uris { get; set; }
        public IEnumerable<string> grants { get; set; }
        public int access_token_lifetime { get; set; }
        public int refresh_token_lifetime { get; set; }
        public string introspection_endpoint_auth_method { get; set; }
        public string revocation_endpoint_auth_method { get; set; }
    }

    public class Permissionstrategy
    {
        public bool enabled { get; set; }
        public string defaultStrategy { get; set; }
        public string allowPolicyId { get; set; }
        public string denyPolicyId { get; set; }
    }

}