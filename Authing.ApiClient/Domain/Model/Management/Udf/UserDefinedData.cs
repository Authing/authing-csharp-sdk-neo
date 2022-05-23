using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UserDefinedData
    {
        #region members
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("dataType")]
        public UdfDataType DataType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
        #endregion
    }
}
