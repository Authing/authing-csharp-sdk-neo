using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management
{
    public class ManagementTest : BaseTest
    {
        [Fact]
        public async void RequestToken_Test()
        {
            var client = managementClient;

            string result = await managementClient.RequestToken();
            Assert.True(!string.IsNullOrEmpty(result));
        }

        [Fact]
        public async void IsPasswordValid_Test()
        {
            var client = managementClient;

            var result = await client.isPasswordValid("386664");

            Assert.True(result.Code == 200);
        }
    }
}
