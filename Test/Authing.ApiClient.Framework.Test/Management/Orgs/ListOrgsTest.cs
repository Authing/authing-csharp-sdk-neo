using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ListOrgsTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试不通过
        /// </summary>
        [Fact]
        public async void ListOrgs_Test()
        {
            var client = managementClient;

            //TODO:该方法结果{"errors":[{"message":{"uniqueId":"de80fa95-436a-4002-97b6-42fb410e6035","code":502,
            //"statusCode":502,"message":"内部服务调用出错"},
            //"locations":[{"line":3,"column":11}],
            //"path":["orgs"],"extensions":{}}],"data":null}
            var result = await client.Orgs.List();

            Assert.True(result.TotalCount == 10);
        }
    }
}