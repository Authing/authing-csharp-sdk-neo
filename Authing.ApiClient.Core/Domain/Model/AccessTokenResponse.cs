using Newtonsoft.Json;

namespace Authing.ApiClient.Core.Domain.Model
{
    public class AccessTokenResponse
    {

        [JsonProperty("accessToken")]
        public AccessTokenRes Result { get; set; }
    }
}