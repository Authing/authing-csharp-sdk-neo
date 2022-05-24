using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class FindByIdTest : BaseTest
    {
        [Fact]
        public async void FindByid_Test()
        {
            var client = managementClient;

            var org = await client.Orgs.Create("获取组织机构详情添加", "详情描述", "9527");

            var orgDetail = await client.Orgs.FindById(org.RootNode.OrgId);

            Assert.True(orgDetail.RootNode.Name == "获取组织机构详情添加");
        }
    }
}