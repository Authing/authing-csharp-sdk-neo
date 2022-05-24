using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class CreateUserResponse
    {
        [JsonProperty("createUser")]
        public User Result { get; set; }
    }
}