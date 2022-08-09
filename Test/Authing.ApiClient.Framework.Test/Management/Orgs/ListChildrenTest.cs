﻿using Authing.ApiClient.Domain.Model.Management.Orgs;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ListChildrenTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListChildren_Test()
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

            var nodeList = await client.Orgs.SearchNodes("第一个节点");

            string nodeId = nodeList.FirstOrDefault().Id;

            var listNode = await client.Orgs.ListChildren(sourceOrg.RootNode.OrgId, nodeId);

            Assert.NotNull(listNode);
        }
    }
}