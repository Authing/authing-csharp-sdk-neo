using System;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class CreateTenantOption
    {
        public string Name { get; set; }
        public string AppIds { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
    }
}
