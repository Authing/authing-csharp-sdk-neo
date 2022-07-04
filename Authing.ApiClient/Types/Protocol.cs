using System.ComponentModel;

namespace Authing.ApiClient.Types
{
    public enum Protocol
    {
        [Description("oidc")]
        OIDC,
        [Description("oauth")]
        OAUTH,
        [Description("saml")]
        SAML,
        [Description("cas")]
        CAS,
        [Description("azure-ad")]
        AZURE_AD
    }
}