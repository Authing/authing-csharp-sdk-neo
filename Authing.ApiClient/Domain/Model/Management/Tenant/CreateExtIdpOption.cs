using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class CreateExtIdpOption
    {
        public string TenantId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ExtIdpType Type { get; set; }
        public ExtIdpConnDetailInput[] Connections { get; set; }
    }
}