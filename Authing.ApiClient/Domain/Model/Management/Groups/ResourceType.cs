using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
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