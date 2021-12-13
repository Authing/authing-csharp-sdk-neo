using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ResourceParam
    {
        [JsonProperty("code")] public string Code { get; set; } = "";
        [JsonProperty("type")] public ResourceType Type { get; set; }
        [JsonProperty("description")] public string Description { get; set; } = "";
        [JsonProperty("actions")] public IEnumerable<ResourceAction> Actions { get; set; } = new List<ResourceAction>();
        [JsonProperty("namespace")] public string NameSpace { get; set; } = "";
        [JsonProperty("apiIdentifier")] public string ApiIdentifier { get; set; } = "";
    }
}