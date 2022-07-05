using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.Library.Domain.Model.Exceptions;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class AddNodeTest : BaseTest
    {
        [Fact]
        public async void Add_Node_Test()
        {
            var client = managementClient;

            var org = await client.Orgs.Create("测试添加的组织节点3");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            AddNodeParam param = new AddNodeParam(org.Nodes.First().OrgId, org.Nodes.First().Id, "测试添加的子节点2");

            var nodeResult = await client.Orgs.AddNode(org.Nodes.First().OrgId, param,authingErrorBox);

            Assert.NotNull(nodeResult);
        }
    }
}