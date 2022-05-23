using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public enum UdfDataType
    {
        [JsonProperty("STRING")]
        STRING,
        [JsonProperty("NUMBER")]
        NUMBER,
        [JsonProperty("DATETIME")]
        DATETIME,
        [JsonProperty("BOOLEAN")]
        BOOLEAN,
        [JsonProperty("OBJECT")]
        OBJECT
    }
}