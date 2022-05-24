using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class AuthorizedResources : BaseTest
    {
        [Fact]
        public async void ListAuthorizedResources_Test()
        {
            var client = managementClient;

            string roleCode = "admin";

            string nameSpace = "613189b38b6c66cac1d211bd";

            var result = await client.Roles.ListAuthorizedResources(roleCode, nameSpace, Types.ResourceType.DATA);

            Assert.True(result.AuthorizedResources.TotalCount == 1);
        }
    }
}