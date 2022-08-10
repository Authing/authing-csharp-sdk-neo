using Authing.ApiClient.Domain.Model.Management.Orgs;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class MoveNodeTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void MoveNode_Test()
        {
            var client = managementClient;

            var sourceOrg = await client.Orgs.Create("测试移动节点组织结构");

            AddNodeParam firstNodeParam = new AddNodeParam(sourceOrg.RootNode.OrgId, sourceOrg.RootNode.Id, "第一个节点");

            await client.Orgs.AddNode(sourceOrg.RootNode.OrgId, firstNodeParam);

            AddNodeParam secondNodeParam = new AddNodeParam(sourceOrg.RootNode.OrgId, sourceOrg.RootNode.Id, "第二个节点");

            var org = await client.Orgs.AddNode(sourceOrg.RootNode.OrgId, secondNodeParam);

            var firstNode = org.Nodes.Where(p => p.Name == "第一个节点").FirstOrDefault();

            AddNodeParam firstChildNodeParam = new AddNodeParam(sourceOrg.RootNode.OrgId, firstNode.Id, "第一个节点的子节点");

            await client.Orgs.AddNode(sourceOrg.RootNode.OrgId, firstChildNodeParam);

            var nodeList = await client.Orgs.SearchNodes("第一个节点的子节点");

            string childNodeId = nodeList.FirstOrDefault().Id;

            var secondeList = await client.Orgs.SearchNodes("第二个节点");

            string secondNodeId = secondeList.FirstOrDefault().Id;

            await client.Orgs.MoveNode(sourceOrg.RootNode.OrgId, childNodeId, secondNodeId);

            var childNode = await client.Orgs.FindNodeById(childNodeId);

            Assert.NotNull(childNode);
        }
    }
}