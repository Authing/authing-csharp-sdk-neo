using Authing.ApiClient.Interfaces.ManagementClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class MFAManagementClient : IMFAManagementClient
    {
        private ManagementClient client;

        public MFAManagementClient(ManagementClient client)
        {
            this.client = client;
        }
    }
}
