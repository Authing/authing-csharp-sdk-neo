using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Udf
{
    public class ManagementUdfRemoveTest : BaseTest
    {
        /// <summary>
        /// 2022-8-8 测试通过
        /// </summary>
        [Fact]
        public async void RemoveUserDefinedField_User()
        {
            var client = managementClient;

            CommonMessage result = await client.Udf.Remove(UdfTargetType.USER, "user");

            Assert.NotNull(result.Code == 200);
        }

        //[Fact]
        //public async void SetUserDefinedField_Role()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.ROLE, "role", Core.Model.UdfDataType.STRING, "12313131");

        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async void SetUserDefineField_Application()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.APPLICATION, "app", Core.Model.UdfDataType.STRING, "13123123");

        //    var key = await client.Udf.UserLogsInfo(UdfTargetType.APPLICATION);

        //    Assert.NotNull(key.First().Key == "app");
        //}

        //[Fact]
        //public async void SetUserDefineField_Node()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.NODE, "node", Core.Model.UdfDataType.STRING, "nodestring");

        //    var key = await client.Udf.UserLogsInfo(UdfTargetType.NODE);

        //    Assert.NotNull(key.First().Key == "node");
        //}

        //[Fact]
        //public async void SetUserDefineField_Org()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.ORG, "org", Core.Model.UdfDataType.STRING, "orgString");

        //    var key = await client.Udf.UserLogsInfo(UdfTargetType.ORG);

        //    Assert.NotNull(key.First().Key == "org");
        //}

        //[Fact]
        //public async void SetUserDefineField_Permission()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.PERMISSION, "org", Core.Model.UdfDataType.STRING, "orgString");

        //    var key = await client.Udf.UserLogsInfo(UdfTargetType.ORG);

        //    Assert.NotNull(key.First().Key == "org");
        //}

        //[Fact]
        //public async void SetUserDefineField_UserPool()
        //{
        //    var client = managementClient;

        //    UserDefinedField result = await client.Udf.Set(UdfTargetType.USERPOOL, "userPool", Core.Model.UdfDataType.STRING, "userPoolString");

        //    var key = await client.Udf.UserLogsInfo(UdfTargetType.USERPOOL);

        //    Assert.NotNull(key.First().Key == "userPool");
        //}
    }
}