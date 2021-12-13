using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AllowResponse
    {

        [JsonProperty("allow")]
        public CommonMessage Result { get; set; }
    }
}