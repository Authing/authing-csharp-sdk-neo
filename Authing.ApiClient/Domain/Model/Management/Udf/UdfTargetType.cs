using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public enum UdfTargetType
    {
        [JsonProperty("NODE")]
        NODE,
        [JsonProperty("ORG")]
        ORG,
        [JsonProperty("USER")]
        USER,
        [JsonProperty("USERPOOL")]
        USERPOOL,
        [JsonProperty("ROLE")]
        ROLE,
        [JsonProperty("PERMISSION")]
        PERMISSION,
        [JsonProperty("APPLICATION")]
        APPLICATION
    }
}
