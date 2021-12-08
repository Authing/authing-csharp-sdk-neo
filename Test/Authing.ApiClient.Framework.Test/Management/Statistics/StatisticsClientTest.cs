using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Domain.Model.Management.Statistics;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Statistics
{
    public class StatisticsClientTest:BaseTest
    {
        [Fact]
        public async Task Statistics_UserLogs()
        {
            var result = await managementClient.Statistics.
                listUserActions(new LogsPageParam()
                {
                });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Statistics_listAuditLogs()
        {
            var result = await managementClient.Statistics.
                listAuditLogs(new AuditLogPageParam()
                {
                });
            Assert.NotEmpty(result.List);
        }
    }
}
