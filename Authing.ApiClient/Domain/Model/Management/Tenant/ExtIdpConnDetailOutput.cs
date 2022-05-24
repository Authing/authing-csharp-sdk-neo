using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ExtIdpConnDetailOutput
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
    }
}