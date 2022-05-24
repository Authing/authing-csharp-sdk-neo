using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class AppAccessPolicy
    {
        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifiers")]
        public string[] TargetIdentifiers { get; set; }

        [JsonProperty("namespace")]
        public string NameSpace { get; set; }

        [JsonProperty("inheritByChildren")]
        public bool InheritByChildren { get; set; }
    }
}