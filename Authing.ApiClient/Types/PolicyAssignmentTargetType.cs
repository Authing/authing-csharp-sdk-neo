using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public enum PolicyAssignmentTargetType
    {
        [JsonProperty("USER")]
        USER,
        [JsonProperty("ROLE")]
        ROLE,
        [JsonProperty("GROUP")]
        GROUP,
        [JsonProperty("ORG")]
        ORG,
        [JsonProperty("AK_SK")]
        AK_SK
    }
}
