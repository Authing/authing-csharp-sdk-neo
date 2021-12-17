using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
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

    }
}
