namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AppAccessPolicyQueryFilter
    {
        public string AppId { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}