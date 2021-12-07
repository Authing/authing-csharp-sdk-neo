using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class AccessTokenRes
    {
        #region members     
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("exp")]
        public int? Exp { get; set; }

        [JsonProperty("iat")]
        public int? Iat { get; set; }
        #endregion
    }
}