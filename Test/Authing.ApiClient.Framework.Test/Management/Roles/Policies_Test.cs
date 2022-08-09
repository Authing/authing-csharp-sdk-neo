using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    /// <summary>
    /// 2022-8-9 测试通过
    /// </summary>
    public class Policies_Test : BaseTest
    {
        [Fact]
        public async void listPolicies_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

            var result = await client.Roles.ListPolicies(roleCode);

            Assert.True(result.TotalCount > 0);
        }

        [Fact]
        public async void AddPolicies_Test()
        {
            var client = managementClient;

            string roleCode = "admin";

            string nameSpace = "613189b38b6c66cac1d211bd";

            //TODO: 未实现
        }

        [Fact]
        public async void RemovePolicies_Test()
        {
            //TODO: 未实现
        }
    }
}