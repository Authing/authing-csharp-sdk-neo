namespace Authing.ApiClient.Types
{
    public class OidcOption : IProtocolInterface
    {
        public string AppId { get; set; }
        public string RedirectUri { get; set; }
#nullable enable
        public ResponseType? ResponseType { get; set; } = null;
        public ResponseMode? ResponseMode { get; set; } = null;
#nullable disable
        public string State { get; set; }
        public string Nonce { get; set; }
        public string Scope { get; set; }
        public CodeChallengeDigestMethod? CodeChallengeMethod { get; set; }
        public string CodeChallenge { get; set; }
    }
}