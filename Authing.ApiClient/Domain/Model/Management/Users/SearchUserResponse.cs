using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class SearchUserResponse
    {

        [JsonProperty("searchUser")]
        public PaginatedUsers Result { get; set; }
    }
}