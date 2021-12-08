using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class PolicyAssignment
    {
        #region members
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("targetType")]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifier")]
        public string TargetIdentifier { get; set; }
        #endregion
    }
}
