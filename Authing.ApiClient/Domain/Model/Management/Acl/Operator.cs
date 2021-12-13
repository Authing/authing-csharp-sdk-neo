using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public enum Operator
    {
        [JsonProperty("AND")]
        AND,
        [JsonProperty("OR")]
        OR
    }
}