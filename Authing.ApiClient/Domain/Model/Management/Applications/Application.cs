using System;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class Application
    {
        public Loginfailcheck loginFailCheck { get; set; }
        public Loginpasswordfailcheck loginPasswordFailCheck { get; set; }
        public Frequentregistercheck frequentRegisterCheck { get; set; }
        public Qrcodeloginstrategy qrcodeLoginStrategy { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool isDemo { get; set; }
        public object initLoginUrl { get; set; }
        public Passwordtabconfig passwordTabConfig { get; set; }
        public Verifycodetabconfig verifyCodeTabConfig { get; set; }
        public object[] complateFiledsPlace { get; set; }
        public bool skipComplateFileds { get; set; }
        public bool cssEnabled { get; set; }
        public object oidcJWEConfig { get; set; }
        public object asaConfig { get; set; }
        public bool enableDeviceMutualExclusion { get; set; }
        public bool isIntegrate { get; set; }
        public bool ssoEnabled { get; set; }
        public object ssoOpenAt { get; set; }
        public object template { get; set; }
        public bool casExpireBaseBrowser { get; set; }
        public bool isAsa { get; set; }
        public string applicationType { get; set; }
        public string loadingBackground { get; set; }
        public bool customSecurityEnabled { get; set; }
        public string loginFailStrategy { get; set; }
        public bool emailVerifiedDefault { get; set; }
        public bool customBrandingEnabled { get; set; }
        public bool sendWelcomeEmail { get; set; }
        public bool uploadedApn { get; set; }


        public QrcodeScanning QrcodeScanning { get; set; }
        public string Id { get; set; }
        public string UserPoolId { get; set; }
        public string Protocol { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string Identifier { get; set; }
        public string Domain { get; set; }

        public KeyValuePair<string, string> Jwks { get; set; }

        public string[] RedirectUris { get; set; }
        public string Css { get; set; }

        public OidcConfig OidcConfig { get; set; }
        public OauthConfig OauthConfig { get; set; }
        public string CreateAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Description { get; set; }
        public object? SsoPageCustomizationSettings { get; set; }
        public string Logo { get; set; }
        public object? LogoutRedirectUris { get; set; }
        public object? LoginTabs { get; set; }

        public string DefaultLoginTab { get; set; }

        public object RegisterTabs { get; set; }
        public string DefaultRegisterTab { get; set; }
        public object? LdapConnections { get; set; }
        public object? AdConnections { get; set; }
        public object? DisabledSocialConnections { get; set; }
        public object? DisabledOidcConnections { get; set; }
        public object? DisabledSamlConnections { get; set; }
        public object? DisabledOauth2Connections { get; set; }
        public object? DisabledCasConnections { get; set; }
        public object? DisabledAzureAdConnections { get; set; }
        public object? ExtendsFields { get; set; }
        public object? Ext { get; set; }
        public object? SamlConfig { get; set; }
        public object? CasConfig { get; set; }
        public bool skipMfa { get; set; }
        public PermissionStrategy PermissionStrategy { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
        public bool OidcProviderEnabled { get; set; }
        public bool OauthProviderEnabled { get; set; }
        public bool SamlProviderEnabled { get; set; }
        public bool CasProviderEnabled { get; set; }
        public bool RegisterDisabled { get; set; }
        public bool ExtendsFieldsEnabled { get; set; }
        public bool ShowAuthorizationPage { get; set; }
        public bool EnableSubAccount { get; set; }
        public bool LoginRequireEmailVerified { get; set; }
        public bool AgreementEnabled { get; set; }
        public string AppType { get; set; }


    }

    public class Passwordtabconfig
    {
        public string[] enabledLoginMethods { get; set; }
    }

    public class Verifycodetabconfig
    {
        public string[] enabledLoginMethods { get; set; }
    }

    public class Loginfailcheck
    {
        public int timeInterval { get; set; }
        public int limit { get; set; }
        public bool enabled { get; set; }
    }

    public class Loginpasswordfailcheck
    {
        public int timeInterval { get; set; }
        public int limit { get; set; }
        public bool enabled { get; set; }
    }

    public class Frequentregistercheck
    {
        public int timeInterval { get; set; }
        public int limit { get; set; }
        public bool enabled { get; set; }
    }

    public class Qrcodeloginstrategy
    {
        public int qrcodeExpiresAfter { get; set; }
        public int ticketExpiresAfter { get; set; }
        public bool returnFullUserInfo { get; set; }
        public bool allowExchangeUserInfoFromBrowser { get; set; }
    }
}
