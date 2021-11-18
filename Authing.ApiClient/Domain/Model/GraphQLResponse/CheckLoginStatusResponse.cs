using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class CheckLoginStatusResponse
    {

        [JsonProperty("checkLoginStatus")]
        public JWTTokenStatus Result { get; set; }
    }
}