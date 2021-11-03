using Newtonsoft.Json;

namespace Authing.ApiClient.Core.Model
{
    public enum UdfDataType
    {
        [JsonProperty("STRING")]
        String,
        [JsonProperty("NUMBER")]
        Number,
        [JsonProperty("DATETIME")]
        Datetime,
        [JsonProperty("BOOLEAN")]
        Boolean,
        [JsonProperty("OBJECT")]
        Object
    }
}