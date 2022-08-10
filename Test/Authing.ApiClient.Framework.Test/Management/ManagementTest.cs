using Xunit;

namespace Authing.ApiClient.Framework.Test.Management
{
    public class ManagementTest : BaseTest
    {
        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void RequestToken_Test()
        {
            var client = managementClient;

            string result = await managementClient.RequestToken();
            Assert.True(!string.IsNullOrEmpty(result));
        }
        /// <summary>
        /// 2022-7-27 测试失败
        /// TODO:密码解密失败
        /// {"uniqueId":"870851ab-a76d-4305-9802-59efdacf805d","code":400,"statusCode":499,"apiCode":400,"message":"密码解密失败"}
        /// </summary>
        [Fact]
        public async void IsPasswordValid_Test()
        {
            var client = managementClient;

            var result = await client.IsPasswordValid("386664");

            Assert.True(result.statusCode == 200);
        }

        [Fact]
        public void InitTest()
        {
            managementClient = new Domain.Client.Impl.ManagementBaseClient.ManagementClient("613189b2eed393affbbf396e", "ccf4951a33e5d54d64e145782a65f0a7");

           var result= managementClient.Users.List().Result;

            Assert.NotNull(result);
        }

        [Fact]
        public void InitTest2()
        {
            managementClient = new Domain.Client.Impl.ManagementBaseClient.ManagementClient(init =>
            {
                init.UserPoolId = "613189b2eed393affbbf396e";
                init.Secret = "ccf4951a33e5d54d64e145782a65f0a7";
            });

            var result = managementClient.Users.List().Result;

            Assert.NotNull(result);
        }

        [Fact]
        public async void InitTest3()
        {
            managementClient =await Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient.ManagementClient.InitManagementClient("613189b2eed393affbbf396e", "ccf4951a33e5d54d64e145782a65f0a7");

            var result = managementClient.Users.List().Result;

            Assert.NotNull(result);
        }
    }
}