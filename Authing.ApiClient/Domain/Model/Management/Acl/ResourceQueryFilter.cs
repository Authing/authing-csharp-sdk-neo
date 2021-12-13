using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ResourceQueryFilter
    {
        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 30;

        public ResourceType Type { get; set; }

        public string NameSpaceCode { get; set; } = null;

        public bool FetchAll { get; set; } = false;
    }
}