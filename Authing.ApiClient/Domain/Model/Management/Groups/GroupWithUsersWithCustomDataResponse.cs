using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class GroupWithUsersWithCustomDataResponse
    {
        [JsonProperty("group")]
        public Group Result { get; set; }
    }
}