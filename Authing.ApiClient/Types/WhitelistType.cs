using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public enum WhitelistType
    {
        [JsonProperty("USERNAME")]
        USERNAME = 1 << 0,
        [JsonProperty("EMAIL")]
        EMAIL = 1 << 1,
        [JsonProperty("PHONE")]
        PHONE = 1 << 2,
    }
}
