using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Infrastructure.GraphQL;
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

            var result = await client.Orgs.ExportByOrgId("629871ba7e4f930974879472");

            Assert.NotNull(result);
        }
    }
}