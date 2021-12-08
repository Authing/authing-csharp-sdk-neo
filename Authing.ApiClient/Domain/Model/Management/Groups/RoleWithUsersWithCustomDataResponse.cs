using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class RoleWithUsersWithCustomDataResponse
    {

        [JsonProperty("role")]
        public Role Result { get; set; }
    }
}