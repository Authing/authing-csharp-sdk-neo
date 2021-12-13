using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class RevokeResourceOpt
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PolicyAssignmentTargetType TargetType { get; set; }
        public string TargetIdentifier { get; set; }
    }
}