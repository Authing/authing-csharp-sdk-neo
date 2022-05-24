using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ExportAllTest : BaseTest
    {
        [Fact]
        public async void ExportAll_Test()
        {
            var client = managementClient;

            var ss = await client.Orgs.List();

            var result = await client.Orgs.ExportAll();

            Assert.NotNull(result);
        }

        [Fact]
        public async void ExportByOrgId_Test()
        {
            var client = managementClient;

            var result = await client.Orgs.ExportByOrgId("61af03a04c01888ddc24f5bb");

            Assert.NotNull(result);
        }
    }
}