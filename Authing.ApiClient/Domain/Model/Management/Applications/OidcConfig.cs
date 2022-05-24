namespace Authing.ApiClient.Domain.Model.Management.Applications
{
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
}