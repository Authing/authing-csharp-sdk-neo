using System;
using Authing.ApiClient.Domain.Model.Management.Tenant;
namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class ApplicationTenantDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Protocol { get; set; }
        public string IsIntegrate { get; set; }
        public TenantInfo[] Tenants { get; set; }
    }
}
