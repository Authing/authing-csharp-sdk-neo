namespace Authing.ApiClient.Types
{
    public class CodeChallengeDigestOption
    {
        public string CodeChallenge { get; set; }
        public CodeChallengeDigestMethod Method { get; set; } = CodeChallengeDigestMethod.S256;
    }
}