using Authing.ApiClient.Domain.Model.Management.Orgs;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class UpdateNodeTest : BaseTest
    {
        [Fact]
        public async Task UpdateNode_TestAsync()
        {
            var client = managementClient;

            var org = await client.Orgs.Create("测试更新添加的组织结构");

            AddNodeParam addNodeParam = new AddNodeParam(org.Nodes.First().OrgId, org.Nodes.First().Id, "测试更新添加的子节点");

            var addOrg = await client.Orgs.AddNode(org.RootNode.OrgId, addNodeParam);

            UpdateNodeParam updateNodeParam = new UpdateNodeParam(addOrg.Nodes.Last().Id)
            {
                Name = "修改后的子节点"
            };

            var updateNode = await client.Orgs.UpdateNode(addOrg.RootNode.OrgId, updateNodeParam);

            Assert.True(updateNode.Name == "修改后的子节点");
        }
    }
}