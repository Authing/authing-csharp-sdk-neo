using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ExtIdpConnDetailInput
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ExtIdpConnType Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
    }
}