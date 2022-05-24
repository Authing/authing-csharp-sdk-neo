using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public class SecurityLevel
    {
        [JsonProperty(PropertyName = "email")] public bool Email { get; set; }

        [JsonProperty(PropertyName = "mfa")] public bool Mfa { get; set; }

        [JsonProperty(PropertyName = "password")]
        public bool Password { get; set; }

        [JsonProperty(PropertyName = "phone")] public bool Phone { get; set; }

        [JsonProperty(PropertyName = "passwordSecurityLevel")]
        public PasswordSecurityLevel PasswordSecurityLevel { get; set; }

        [JsonProperty(PropertyName = "score")] public int Score { get; set; }
    }
}