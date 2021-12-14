using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AppAccessPolicy
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifiers")]
        public IEnumerable<string> TartgetIdentifiers { get; set; }

        [JsonProperty("namespace")]
        public string NameSpace { get; set; }

        [JsonProperty("inheritByChildren")]
        public bool? InheritByChildren { get; set; }
    }
}