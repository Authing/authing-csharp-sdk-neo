using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management
{
    public class ListUsersOption
    {
        public string NameSpace { get; set; }
        public bool WithCustomData { get; set; } = false;

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
