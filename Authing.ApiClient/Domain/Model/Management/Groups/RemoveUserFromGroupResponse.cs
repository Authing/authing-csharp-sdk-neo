using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class RemoveUserFromGroupResponse
    {

        [JsonProperty("removeUserFromGroup")]
        public CommonMessage Result { get; set; }
    }
}