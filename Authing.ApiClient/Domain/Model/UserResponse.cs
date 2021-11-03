using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UserResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}