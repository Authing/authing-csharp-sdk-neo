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
    }
}