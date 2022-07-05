using Authing.ApiClient.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Model.Authentication
{
    public class EnterpriseAuthenticationOptions
    {
        public Action<User> OnSuccess { get; set; }

        public Action<int,string> OnError { get; set; }
    }
}
