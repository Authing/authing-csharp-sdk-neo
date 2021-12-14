using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public enum PolicyEffect
    {
        [JsonProperty("ALLOW")]
        ALLOW,

        [JsonProperty("DENY")]
        DENY
    }
}