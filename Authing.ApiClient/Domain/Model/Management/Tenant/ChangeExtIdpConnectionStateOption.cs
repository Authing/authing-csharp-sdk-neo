namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ChangeExtIdpConnectionStateOption
    {
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public bool Enabled { get; set; }
    }
}