using Authing.ApiClient.Domain.Model.Management.Orgs;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class DeteleNodeTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteNode_Test()
        {
            var client = managementClient;

            var org = await client.Orgs.Create("测试删除子节点", "详情", "9527");

            Authing.ApiClient.Domain.Model.Management.Orgs.Org newOrg = null;
            for (int i = 0; i < 10; i++)
            {
                AddNodeParam addNodeParam = new AddNodeParam(org.RootNode.OrgId, org.RootNode.Id, "测试删除子节点添加的节点" + i);

                newOrg = await client.Orgs.AddNode(org.RootNode.OrgId, addNodeParam);
            }

            List<Domain.Model.Management.Orgs.Node> nodeList = newOrg.Nodes.Where(p => p.Root == false).ToList();

            for (int i = 0; i < nodeList.Count(); i++)
            {
                var mes = await client.Orgs.DeleteNode(newOrg.RootNode.OrgId, nodeList[i].Id);

                Assert.True(mes.Code == 200);
            }
        }
    }
}