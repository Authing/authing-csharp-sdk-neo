using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class PermissionStrategy
    {
        public string AllowPolicyId { get; set; }

        public string DenyPolicyId { get; set; }

        public bool Enabled { get; set; }

        public string DefaultStrategy { get; set; }
    }
}
