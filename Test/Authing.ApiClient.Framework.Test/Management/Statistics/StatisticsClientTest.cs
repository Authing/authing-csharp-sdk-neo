using System.Collections.Generic;
using System.Threading.Tasks;
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
                    UserId = new List<string>() { "61728068c7e3e10ca9cbe8a9" }
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
