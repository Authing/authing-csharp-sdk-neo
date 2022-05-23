using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UserDefinedField
    {
        #region members
        [JsonProperty("targetType")]
        public UdfTargetType TargetType { get; set; }

        [JsonProperty("dataType")]
        public UdfDataType DataType { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("options")]
        public string Options { get; set; }
        #endregion
    }
}