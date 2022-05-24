using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class JWTTokenStatus
    {
        #region members
        [JsonProperty("code")]
        public int? Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }

        [JsonProperty("exp")]
        public int? Exp { get; set; }

        [JsonProperty("iat")]
        public int? Iat { get; set; }

        [JsonProperty("data")]
        public JWTTokenStatusDetail Data { get; set; }
        #endregion
    }
}