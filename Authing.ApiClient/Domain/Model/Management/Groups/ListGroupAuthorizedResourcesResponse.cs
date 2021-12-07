using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class ListGroupAuthorizedResourcesResponse
    {

        [JsonProperty("group")]
        public Group Result { get; set; }
    }
}