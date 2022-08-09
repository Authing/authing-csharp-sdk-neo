using Authing.ApiClient.Domain.Model.Management.Statistics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Statistics
{
    public class StatisticsClientTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async Task Statistics_UserLogs()
        {
            var result = await managementClient.Statistics.
                listUserActions(new LogsPageParam()
                {
                    UserId = new List<string>() { "61a5c55fc89ff91083293e45" }
                });
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
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