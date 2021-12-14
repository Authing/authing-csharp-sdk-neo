using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class RevokeResourceParams
    {
        public string NameSpace { get; set; }
        public string Resource { get; set; }
        public IEnumerable<RevokeResourceOpt> Opts { get; set; } = new List<RevokeResourceOpt>();
    }
}