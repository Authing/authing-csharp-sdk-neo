using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class AuthorizedResources : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListAuthorizedResources_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

            var result = await client.Roles.ListAuthorizedResources(roleCode, nameSpace, Types.ResourceType.DATA);

            Assert.True(result.AuthorizedResources.TotalCount == 1);
        }
    }
}