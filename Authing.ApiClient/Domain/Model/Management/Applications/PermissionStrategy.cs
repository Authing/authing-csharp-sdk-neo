namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class PermissionStrategy
    {
        public string AllowPolicyId { get; set; }

        public string DenyPolicyId { get; set; }

        public bool Enabled { get; set; }

        public string DefaultStrategy { get; set; }
    }
}