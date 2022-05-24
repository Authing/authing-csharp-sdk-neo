using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Authing.ApiClient.Types
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ListOrgsRes
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public IEnumerable<object> Data { get; set; }
    }
}