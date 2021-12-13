using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ResourcePermissionAssignment
    {
        #region members
        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }
        #endregion
    }
}