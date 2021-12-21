using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class SetUdfValueBatchResponse
    {

        [JsonProperty("setUdfValueBatch")]
        public CommonMessage Result { get; set; }
    }
}
