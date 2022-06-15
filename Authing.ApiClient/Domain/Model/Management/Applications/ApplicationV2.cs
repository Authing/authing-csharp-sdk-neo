namespace Authing.Library.Domain.Model.Management.Applications
{
    public class ApplicationV2
    {
        public object[] allowedOrigins { get; set; }
        public string[] corsWhitelist { get; set; }
        public bool userPoolInWhitelist { get; set; }
        public bool skipComplateFileds { get; set; }
        public object[] complateFiledsPlace { get; set; }
        public object[] extendsFields { get; set; }
        public bool extendsFieldsEnabled { get; set; }
        public string cdnBase { get; set; }
        public string id { get; set; }
        public object template { get; set; }
        public string userPoolId { get; set; }
        public object description { get; set; }
        public string identifier { get; set; }
        public bool showAuthorizationPage { get; set; }
        public string publicKey { get; set; }
        public string name { get; set; }
        public string css { get; set; }
        public string logo { get; set; }
        public string userpoolLogo { get; set; }
        public string userpoolName { get; set; }
        public Ssopagecomponentdisplay ssoPageComponentDisplay { get; set; }
        public bool registerDisabled { get; set; }
        public Logintabs loginTabs { get; set; }
        public Qrcodetabssettings qrcodeTabsSettings { get; set; }
        public Registertabs registerTabs { get; set; }
        public object[] socialConnections { get; set; }
        public object[] ecConnections { get; set; }
        public object[] identityProviders { get; set; }
        public string[] redirectUris { get; set; }
        public object[] logoutRedirectUris { get; set; }
        public string protocol { get; set; }
        public Oidcconfig oidcConfig { get; set; }
        public Oauthconfig oauthConfig { get; set; }
        public object samlConfig { get; set; }
        public object casConfig { get; set; }
        public string rootUserPoolId { get; set; }
        public bool enableSubAccount { get; set; }
        public int packageType { get; set; }
        public bool customBrandingEnabled { get; set; }
        public Userportal userPortal { get; set; }
        public string websocket { get; set; }
        public int verifyCodeLength { get; set; }
        public bool agreementEnabled { get; set; }
        public object[] agreements { get; set; }
        public int passwordStrength { get; set; }
        public Custompasswordstrength customPasswordStrength { get; set; }
        public Api api { get; set; }
        public bool loginFailCheckEnabled { get; set; }
        public Passwordtabconfig passwordTabConfig { get; set; }
        public Verifycodetabconfig verifyCodeTabConfig { get; set; }
        public Changeemailstrategy changeEmailStrategy { get; set; }
        public Changephonestrategy changePhoneStrategy { get; set; }
        public string userPoolType { get; set; }
        public object sceneCode { get; set; }
        public object welcomeMessage { get; set; }
        public Docs docs { get; set; }
        public string requestHostname { get; set; }
        public object customLoading { get; set; }
        public object internationalSmsConfig { get; set; }
        public string loadingBackground { get; set; }
        public Asa asa { get; set; }
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
        public string[] list { get; set; }
        public string _default { get; set; }
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
        public string[] list { get; set; }
        public string _default { get; set; }
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