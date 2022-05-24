using System.Collections.Generic;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class CreateExtIdpConnectionOption
    {
        public string ExtIdpId { get; set; }
        public ExtIdpConnType Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
        public string Logo { get; set; }
    }
}