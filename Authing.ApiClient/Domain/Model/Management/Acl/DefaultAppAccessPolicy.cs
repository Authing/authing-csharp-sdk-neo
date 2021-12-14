namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class DefaultAppAccessPolicy
    {
        public string AppId { get; set; }
        public DefaultStrategy DefaultStrategy { get; set; }
    }
}