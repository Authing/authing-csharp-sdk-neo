using System.Linq;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Infrastructure.GraphQL;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ExportAllTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试不通过
        /// </summary>
        [Fact]
        public async void ExportAll_Test()
        {
            var client = managementClient;

            //TODO:该方法结果{"errors":[{"message":{"uniqueId":"de80fa95-436a-4002-97b6-42fb410e6035","code":502,
            //"statusCode":502,"message":"内部服务调用出错"},
            //"locations":[{"line":3,"column":11}],
            //"path":["orgs"],"extensions":{}}],"data":null}
            //var ss = await client.Orgs.List();

            var result = await client.Orgs.ExportAll();

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ExportByOrgId_Test()
        {
            var client = managementClient;

            var res = await client.Orgs.SearchNodes("Tommy");

            var result = await client.Orgs.ExportByOrgId(res.First().OrgId);

            Assert.NotNull(result);
        }
    }
}