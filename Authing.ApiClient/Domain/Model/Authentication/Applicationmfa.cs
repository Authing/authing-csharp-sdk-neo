using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class Applicationmfa
    {
        [JsonProperty("mfaPolicy")]
        public string MfaPolicy { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("sort")]
        public int Sort { get; set; }
    }
}