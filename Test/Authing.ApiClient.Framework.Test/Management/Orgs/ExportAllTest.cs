using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ExportAllTest : BaseTest
    {
        [Fact]
        public async void ExportAll_Test()
        {
            var client = managementClient;

           var ss=await client.Orgs.List();

            var result = await client.Orgs.ExportAll();

            Assert.NotNull(result);
        }

        [Fact]
        public async void ExportByOrgId_Test()
        {
            var client = managementClient;

            var result = await client.Orgs.ExportByOrgId("61af013d3b63bca568c5192a");

            Assert.NotNull(result);
        }
    }
}
