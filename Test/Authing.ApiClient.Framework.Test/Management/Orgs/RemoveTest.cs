using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class RemoveTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void RemoveOrgs()
        {
            var client = managementClient;

            var node = await client.Orgs.Create("组织节点2", "新增加的组织节点2", "002");

            Assert.NotNull(node);

            var result = await client.Orgs.DeleteById(node.Id);

            Assert.True(result.Code == 200);
        }
    }
}