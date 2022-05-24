using Authing.ApiClient.Domain.Model.Management.Orgs;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class IsRootNodeTest : BaseTest
    {
        [Fact]
        public async void IsRootNode_Test()
        {
            var client = managementClient;

            var sourceOrg = await client.Orgs.Create("测试是否为根节点添加的组织机构");

            AddNodeParam firstNodeParam = new AddNodeParam(sourceOrg.RootNode.OrgId, sourceOrg.RootNode.Id, "第一个节点");

            await client.Orgs.AddNode(sourceOrg.RootNode.OrgId, firstNodeParam);

            var nodeList = await client.Orgs.SearchNodes("第一个节点");

            string childNodeId = nodeList.FirstOrDefault().Id;

            var result = await client.Orgs.IsRootNode(sourceOrg.RootNode.OrgId, sourceOrg.RootNode.Id);

            Assert.True(result);

            result = await client.Orgs.IsRootNode(sourceOrg.RootNode.OrgId, childNodeId);

            Assert.False(result);
        }
    }
}