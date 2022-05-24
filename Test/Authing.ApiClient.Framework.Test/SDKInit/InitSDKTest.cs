using Xunit;

namespace Authing.ApiClient.Framework.Test.SDKInit
{
    public class InitSDKTest : BaseTest
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = managementClient;
            Assert.NotNull(client.Users);
        }

        [Fact]
        public async void should_init_authing_sdk_with_init_option()
        {
            var client = managementClient;
            Assert.NotNull(client.Users);
        }

        [Fact]
        public async void should_init_authing_authentication_sdk_with_init_option()
        {
            var res = await authenticationClient.CheckLoginStatus();
            Assert.Equal(2206, res.Code);
            Assert.Equal(false, res.Status);
            Assert.Equal("登录信息已过期", res.Message);
        }
    }
}