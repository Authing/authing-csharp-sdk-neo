using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UserWithCustomDataResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}