namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Permissionstrategy
    {
        public bool enabled { get; set; }
        public string defaultStrategy { get; set; }
        public string allowPolicyId { get; set; }
        public string denyPolicyId { get; set; }
    }
}