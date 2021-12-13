using System.Collections.Generic;
using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ListResourcesRes
    {
        public IEnumerable<Resources> List { get; set; }
        public int TotalCount { get; set; }
    }
}