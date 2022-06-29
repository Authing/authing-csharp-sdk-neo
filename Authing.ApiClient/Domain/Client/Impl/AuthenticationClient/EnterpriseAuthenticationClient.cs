using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
    public class EnterpriseAuthenticationClient: BaseAuthenticationClient
    {
        public EnterpriseAuthenticationClient(Action<InitAuthenticationClientOptions> init):base(init)
        {

        }


    }
}
