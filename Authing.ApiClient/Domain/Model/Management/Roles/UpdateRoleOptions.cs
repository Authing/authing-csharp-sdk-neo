using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class UpdateRoleOptions
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string NewCode { get; set; }

        public string NameSpace { get; set; }
    }
}
