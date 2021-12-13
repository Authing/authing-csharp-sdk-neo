using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ProgrammaticAccessAccountList
    {
        public int TotalCount { get; set; }
        public IEnumerable<ProgrammaticAccessAccount> List { get; set; }
    }
}