using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class GetAuthorizedTargetsOptions
    {
        [JsonProperty("namespace")]
        public string NameSpace { get; set; }
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("resourceType")]
        public ResourceType? ResourceType { get; set; } = null;
        [JsonProperty("actions")]
        public AuthorizedTargetsActionsInput Actions { get; set; }
        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }
    }
}