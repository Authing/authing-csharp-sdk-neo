namespace Authing.ApiClient.Domain.Model.Management.Applications
{
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
}