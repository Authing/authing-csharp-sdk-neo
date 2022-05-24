using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class FindUserResponse
    {

        [JsonProperty("findUser")]
        public User Result { get; set; }
    }
}