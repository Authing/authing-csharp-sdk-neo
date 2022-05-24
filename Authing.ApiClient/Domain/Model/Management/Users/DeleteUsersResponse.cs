using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class DeleteUsersResponse
    {

        [JsonProperty("deleteUsers")]
        public CommonMessage Result { get; set; }
    }
}