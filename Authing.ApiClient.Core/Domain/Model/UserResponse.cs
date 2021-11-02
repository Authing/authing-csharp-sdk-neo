using Newtonsoft.Json;

namespace Authing.ApiClient.Core.Domain.Model
{
    public class UserResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}