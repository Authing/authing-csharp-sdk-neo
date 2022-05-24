using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Udf
{
    public class ManagementUdfTest : BaseTest
    {
        [Fact]
        public async void SetUserDefinedField_User()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.USER, "asdad", UdfDataType.STRING, "1312312");

            Assert.NotNull(result);
        }

        [Fact]
        public async void SetUserDefinedField_Role()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.ROLE, "role", UdfDataType.STRING, "12313131");

            Assert.NotNull(result);
        }

        [Fact]
        public async void SetUserDefineField_Application()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.APPLICATION, "app", UdfDataType.STRING, "13123123");

            var key = await client.Udf.List(UdfTargetType.APPLICATION);

            Assert.NotNull(key.First().Key == "app");
        }

        [Fact]
        public async void SetUserDefineField_Node()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.NODE, "node", UdfDataType.STRING, "nodestring");

            var key = await client.Udf.List(UdfTargetType.NODE);

            Assert.NotNull(key.First().Key == "node");
        }

        [Fact]
        public async void SetUserDefineField_Org()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.ORG, "org", UdfDataType.STRING, "orgString");

            var key = await client.Udf.List(UdfTargetType.ORG);

            Assert.NotNull(key.First().Key == "org");
        }

        [Fact]
        public async void SetUserDefineField_Permission()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.PERMISSION, "org", UdfDataType.STRING, "orgString");

            var key = await client.Udf.List(UdfTargetType.ORG);

            Assert.NotNull(key.First().Key == "org");
        }

        [Fact]
        public async void SetUserDefineField_UserPool()
        {
            var client = managementClient;

            UserDefinedField result = await client.Udf.Set(UdfTargetType.USERPOOL, "userPool", UdfDataType.STRING, "userPoolString");

            var key = await client.Udf.List(UdfTargetType.USERPOOL);

            Assert.NotNull(key.First().Key == "userPool");
        }
    }
}