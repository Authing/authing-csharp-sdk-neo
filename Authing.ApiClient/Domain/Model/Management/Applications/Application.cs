using System;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class Application
    {
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

    public class ApplicationList
    {
        public int TotalCount { get; set; }
        public Application[] List { get; set; }
    }

    public class App
    {
        public KeyValuePair<string, object> QrcodeScanning { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public object? Description { get; set; }

        public string Identifier { get; set; }

        public string Logo { get; set; }

        public string[] LoginTabs { get; set; }

        public string[] RegisterTabs { get; set; }

        public object[] AdConnections { get; set; }

        public object[] DisabledOidcConnections { get; set; }

        public object[] DisabledSamlConnections { get; set; }

        public object[] ExtendsFields { get; set; }

        public object[] DisabledAzureAdConnections { get; set; }

        public object[] DisabledOauth2Connections { get; set; }

        public object[] DisabledCasConnections { get; set; }
    }

    public class QrcodeScanning
    {
        public bool Redirect { get; set; }
        public int interval { get; set; }
    }

    public class OidcConfig
    {
        public int Id { get; set; }
        public string ClientSecret { get; set; }

        public string[] RedirectUris { get; set; }

        public string[] Grants { get; set; }

        public int AccessTokenLifeTime { get; set; }

        public int RefreshTokenLifetime { get; set; }

        public string IntrospectionEndpointAuthMethod { get; set; }

        public string RevocationEndpointAuthMethod { get; set; }
    }

    public class OauthConfig
    {
        public string[] GrantTypes { get; set; }

        public string[] ResponseTypes { get; set; }

        public string IdTokenSignedResponseAlg { get; set; }

        public object? JwksUri { get; set; }

        public string TokenEndpointAuthMethod { get; set; }

        public object? RequestObjectEncryptionAlg { get; set; }
        public object? RequestObjectSigningAlg { get; set; }
        public object? UserinfoEncryptedResponseEnc { get; set; }
        public object? UserinfoEncryptedResponseAlg { get; set; }
        public object? UserinfoSignedResponseAlg { get; set; }
        public object? IdTokenEncryptedResponseEnc { get; set; }
        public object? IdTokenEncryptedResponseAlg { get; set; }
        public object? Jwks { get; set; }
        public int AuthorizationCodeExpire { get; set; }
        public int IdTokenExpire { get; set; }
        public int AccessTokenExpire { get; set; }
        public int RefreshTokenExpire { get; set; }
        public int CasExpire { get; set; }
        public bool SkipConsent { get; set; }
    }

    public class PermissionStrategy
    {
        public string AllowPolicyId { get; set; }

        public string DenyPolicyId { get; set; }

        public bool Enabled { get; set; }

        public string DefaultStrategy { get; set; }
    }

    public class PublicApplication
    {
        public QrcodeScanning QrcodeScanning { get; set; }
        public string Id { get; set; }
        public string UserPoolId { get; set; }
        public string Protocol { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string Identifier { get; set; }

        public KeyValuePair<string, string> Jwks { get; set; }

        public string[] RedirectUris { get; set; }
        public string Css { get; set; }

        public string CreateAt { get; set; }
        public string UpdatedAt { get; set; }
        public object? Description { get; set; }
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
    }
}
