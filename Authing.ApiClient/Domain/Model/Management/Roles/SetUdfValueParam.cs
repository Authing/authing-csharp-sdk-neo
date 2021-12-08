using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class SetUdfValueParam
    {
        public string RoleId { get; set; }
        public KeyValueDictionary UdvList { get; set; }
    }
}
