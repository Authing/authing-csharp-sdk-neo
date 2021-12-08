using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class AddUserToGroupResponse
    {

        [JsonProperty("addUserToGroup")]
        public CommonMessage Result { get; set; }
    }
}