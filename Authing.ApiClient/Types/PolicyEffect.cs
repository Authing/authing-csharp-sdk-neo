using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum PolicyEffect
    {
        [JsonProperty("ALLOW")]
        ALLOW,
        [JsonProperty("DENY")]
        DENY
    }
}
