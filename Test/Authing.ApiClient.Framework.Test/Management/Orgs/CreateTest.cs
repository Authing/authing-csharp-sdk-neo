using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class CreateTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void CreateOrgs()
        {
            var client = managementClient;

            var result = await client.Orgs.Create("组织结构1", "组织结构1的描述", "9527");

            Assert.NotNull(result);
        }
    }
}