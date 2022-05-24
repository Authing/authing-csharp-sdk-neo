using System;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ExtIdpListOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TenantId { get; set; }
    }
}