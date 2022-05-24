using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class PolicyStatementCondition
    {
        #region members
        [JsonProperty("param")]
        public string Param { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
        #endregion
    }
}