using Newtonsoft.Json;

namespace Authing.ApiClient.Core.Domain.Model
{
    public class UserWithCustomDataResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}