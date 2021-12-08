using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        public StatisticsManagement Statistics { get; set; }

        public class StatisticsManagement
        {
            private readonly ManagementClient client;

            public StatisticsManagement(ManagementClient client)
            {
                this.client = client;
            }
        }
    }
}
