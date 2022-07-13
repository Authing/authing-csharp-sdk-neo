using Authing.ApiClient.Domain.Model.Management.Applications;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Authing.Library.Domain.Model.Management.Applications
{
    public class ApplicationV2
    {
        
        public object[] AllowedOrigins { get; set; }
        public string[] CorsWhitelist { get; set; }
        public bool UserPoolInWhitelist { get; set; }
        public bool SkipComplateFileds { get; set; }
        public object[] ComplateFiledsPlace { get; set; }
        public object[] ExtendsFields { get; set; }
        public bool ExtendsFieldsEnabled { get; set; }
        public string CdnBase { get; set; }
        public string Id { get; set; }
        public object Template { get; set; }
        public string UserPoolId { get; set; }
        public object Description { get; set; }
        public string Identifier { get; set; }
        public bool ShowAuthorizationPage { get; set; }
        public string PublicKey { get; set; }
        public string Name { get; set; }
        public string Css { get; set; }
        public string Logo { get; set; }
        public string UserpoolLogo { get; set; }
        public string UserpoolName { get; set; }
        public Ssopagecomponentdisplay SsoPageComponentDisplay { get; set; }
        public bool RegisterDisabled { get; set; }
        public Logintabs LoginTabs { get; set; }
        public Qrcodetabssettings QrcodeTabsSettings { get; set; }
        public Registertabs RegisterTabs { get; set; }
        public object[] SocialConnections { get; set; }
        public object[] EcConnections { get; set; }
        public object[] IdentityProviders { get; set; }
        public string[] RedirectUris { get; set; }
        public object[] LogoutRedirectUris { get; set; }
        public string Protocol { get; set; }
        public Oidcconfig OidcConfig { get; set; }
        public Oauthconfig OauthConfig { get; set; }
        public object SamlConfig { get; set; }
        public object CasConfig { get; set; }
        public string RootUserPoolId { get; set; }
        public bool EnableSubAccount { get; set; }
        public int PackageType { get; set; }
        public bool CustomBrandingEnabled { get; set; }
        public Userportal UserPortal { get; set; }
        public string Websocket { get; set; }
        public int VerifyCodeLength { get; set; }
        public bool AgreementEnabled { get; set; }
        public List<Agreement> Agreements { get; set; }
        public int PasswordStrength { get; set; }
        public Custompasswordstrength CustomPasswordStrength { get; set; }
        public Api Api { get; set; }
        public bool LoginFailCheckEnabled { get; set; }
        public Passwordtabconfig PasswordTabConfig { get; set; }
        public Verifycodetabconfig VerifyCodeTabConfig { get; set; }
        public Changeemailstrategy ChangeEmailStrategy { get; set; }
        public Changephonestrategy ChangePhoneStrategy { get; set; }
        public string UserPoolType { get; set; }
        public object SceneCode { get; set; }
        public object WelcomeMessage { get; set; }
        public Docs Docs { get; set; }
        public string RequestHostname { get; set; }
        public object CustomLoading { get; set; }
        public object InternationalSmsConfig { get; set; }
        public string LoadingBackground { get; set; }
        public Asa Asa { get; set; }
    }

    public class Ssopagecomponentdisplay
    {
        public bool loginMethodNav { get; set; }
        public bool loginByPhoneCodeTab { get; set; }
        public bool loginByUserPasswordTab { get; set; }
        public bool wxMpScanTab { get; set; }
        public bool userPasswordInput { get; set; }
        public bool phoneCodeInput { get; set; }
        public bool forgetPasswordBtn { get; set; }
        public bool loginBtn { get; set; }
        public bool registerBtn { get; set; }
        public bool socialLoginBtns { get; set; }
        public bool idpBtns { get; set; }
        public bool registerMethodNav { get; set; }
        public bool registerByPhoneTab { get; set; }
        public bool registerByEmailTab { get; set; }
        public bool autoRegisterThenLoginHintInfo { get; set; }
    }

    public class Logintabs
    {
        [JsonProperty("list")]
        public string[] List { get; set; }

        [JsonProperty("default")]
        public string Default { get; set; }
        public Title title { get; set; }
    }

    public class Title
    {
        public string password { get; set; }
        public string phonecode { get; set; }
        public string wechatminiprogramqrcode { get; set; }
        public string appqrcode { get; set; }
        public string ldap { get; set; }
        public string ad { get; set; }
        public string wechatmpqrcode { get; set; }
    }

    public class Qrcodetabssettings
    {
        public object[] wechatmpqrcode { get; set; }
        public object[] wechatminiprogramqrcode { get; set; }
    }

    public class Registertabs
    {
        [JsonProperty("list")]
        public string[] List { get; set; }
        [JsonProperty("default")]
        public string Default { get; set; }
        public Title1 title { get; set; }
    }

    public class Title1
    {
        public string email { get; set; }
        public string emailCode { get; set; }
        public string phone { get; set; }
    }

    public class Oidcconfig
    {
        public string[] grant_types { get; set; }
        public string[] response_types { get; set; }
        public string id_token_signed_response_alg { get; set; }
        public string token_endpoint_auth_method { get; set; }
        public int authorization_code_expire { get; set; }
        public int id_token_expire { get; set; }
        public int access_token_expire { get; set; }
        public int refresh_token_expire { get; set; }
        public int cas_expire { get; set; }
        public bool skip_consent { get; set; }
        public string[] redirect_uris { get; set; }
        public object[] post_logout_redirect_uris { get; set; }
        public string client_id { get; set; }
        public string scope { get; set; }
    }

    public class Oauthconfig
    {
        public string id { get; set; }
        public string[] redirect_uris { get; set; }
        public string[] grants { get; set; }
        public int access_token_lifetime { get; set; }
        public int refresh_token_lifetime { get; set; }
        public string introspection_endpoint_auth_method { get; set; }
        public string revocation_endpoint_auth_method { get; set; }
    }

    public class Userportal
    {
        public string title { get; set; }
        public string favicon { get; set; }
        public string cdnBase { get; set; }
        public string assetsBase { get; set; }
        public string assetsVersion { get; set; }
        public string icpRecord { get; set; }
        public string psbRecord { get; set; }
        public Poweredby poweredBy { get; set; }
        public Guard guard { get; set; }
        public string projectName { get; set; }
        public object[] personalCenterMenu { get; set; }
        public bool enableAuthentication { get; set; }
        public string defaultLang { get; set; }
        public Ga ga { get; set; }
        public Gaguardv1 gaGuardV1 { get; set; }
        public Volcengine volcengine { get; set; }
    }

    public class Poweredby
    {
        public string logo { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Guard
    {
        public string _default { get; set; }
        public string newApp { get; set; }
    }

    public class Ga
    {
        public bool enabled { get; set; }
        public string gTrackingId { get; set; }
    }

    public class Gaguardv1
    {
        public bool enabled { get; set; }
        public string gTrackingId { get; set; }
    }

    public class Volcengine
    {
        public bool enabled { get; set; }
        public int id { get; set; }
    }

    public class Custompasswordstrength
    {
        public bool enabled { get; set; }
        public object regex { get; set; }
        public object message { get; set; }
    }

    public class Api
    {
        public Headers headers { get; set; }
    }

    public class Headers
    {
        public Keys keys { get; set; }
    }

    public class Keys
    {
        public string userpoolid { get; set; }
        public string tenantid { get; set; }
        public string appid { get; set; }
        public string sdkversion { get; set; }
        public string requestfrom { get; set; }
        public string lang { get; set; }
        public string oauthaccesstoken { get; set; }
        public string oauthcode { get; set; }
        public string oidcaccesstoken { get; set; }
        public string oidccode { get; set; }
        public string webhooksecret { get; set; }
        public string token { get; set; }
    }

    public class Passwordtabconfig
    {
        public string[] enabledLoginMethods { get; set; }
    }

    public class Verifycodetabconfig
    {
        public string[] enabledLoginMethods { get; set; }
    }

    public class Changeemailstrategy
    {
        public bool verifyOldEmail { get; set; }
    }

    public class Changephonestrategy
    {
        public bool verifyOldPhone { get; set; }
    }

    public class Docs
    {
        public string host { get; set; }
    }

    public class Asa
    {
        public Plugin plugin { get; set; }
    }

    public class Plugin
    {
        public string version { get; set; }
        public string cdnBase { get; set; }
    }

}