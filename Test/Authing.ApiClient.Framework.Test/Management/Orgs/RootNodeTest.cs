using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class RootNodeTest : BaseTest
    {
        [Fact]
        public async void RootNode_Test()
        {
            var client = managementClient;

            var org = await client.Orgs.Create("测试获取根节点");

            var result = await client.Orgs.RootNode(org.Nodes.First().OrgId);

            Assert.True(result.Root == true);
        }
    }
}