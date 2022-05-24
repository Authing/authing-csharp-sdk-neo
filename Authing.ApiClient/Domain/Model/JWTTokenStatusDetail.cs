using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class JWTTokenStatusDetail
    {
        #region members
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("arn")]
        public string Arn { get; set; }
        #endregion
    }
}