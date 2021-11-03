using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class AccessTokenResponse
    {

        [JsonProperty("accessToken")]
        public AccessTokenRes Result { get; set; }
    }
}