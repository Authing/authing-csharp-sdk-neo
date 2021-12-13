using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class IsActionAllowedResponse
    {
        [JsonProperty("isActionAllowed")]
        public bool Result { get; set; }
    }

}