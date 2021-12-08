using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.WhiteList
{
    public class WhiteList
    {
        #region members
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        #endregion
    }
}
