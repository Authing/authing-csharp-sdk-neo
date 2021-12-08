using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum ResourceType
    {
        [JsonProperty("DATA")]
        DATA,
        [JsonProperty("API")]
        API,
        [JsonProperty("MENU")]
        MENU,
        [JsonProperty("UI")]
        UI,
        [JsonProperty("BUTTON")]
        BUTTON
    }
}
