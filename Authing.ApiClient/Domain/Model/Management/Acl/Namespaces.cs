using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Namespaces
    {
        public int Total { get; set; }

        public IEnumerable<NameSpace> List { get; set; }
    }
}