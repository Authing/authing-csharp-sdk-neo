using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class AuthorizedResourcesTest : BaseTest
    {
        [Fact]
        public async void AuthorizeResourByNodeId_Test()
        {
            var client = managementClient;

            var result = await client.Orgs.ListAuthorizedResourcesByNodeId("61af013d691c6cd83c4a8ac4", "613189b38b6c66cac1d211bd");

            Assert.NotNull(result);
        }

        [Fact]
        public async void AuthorizeResourceByNodeCode_Test()
        {
            var client = managementClient;

            var node = await client.Orgs.SearchNodes("资源机构");

            var result = await client.Orgs.ListAuthorizedResourcesByNodeCode("61af013d090074d1ea8e84bf", "9527", "613189b38b6c66cac1d211bd");

            Assert.NotNull(result);
        }
    }
}