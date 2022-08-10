using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class AuthorizedResourcesTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void AuthorizeResourByNodeId_Test()
        {
            var client = managementClient;

            var result = await client.Orgs.ListAuthorizedResourcesByNodeId("62f0dae660cfae9bd667d5e1", "default");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void AuthorizeResourceByNodeCode_Test()
        {
            var client = managementClient;

            var node = await client.Orgs.SearchNodes("Tommy");

            var result = await client.Orgs.ListAuthorizedResourcesByNodeCode(node.First().OrgId, node.First().Code, "default");

            Assert.NotNull(result);
        }
    }
}