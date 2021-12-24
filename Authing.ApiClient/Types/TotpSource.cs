using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum TotpSource
    {
        [JsonProperty("APPLICATION")]
        APPLICATION,

        [JsonProperty("SELF")]
        SELF
    }
}
