using System;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class TenantMembers
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public User User { get; set; }
    }
}
