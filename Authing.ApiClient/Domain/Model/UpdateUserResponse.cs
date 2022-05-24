using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UpdateUserResponse
    {
        [JsonProperty("updateUser")]
        public User Result { get; set; }
    }
}