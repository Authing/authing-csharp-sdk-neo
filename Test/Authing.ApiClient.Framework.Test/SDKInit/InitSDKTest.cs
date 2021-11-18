using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Test.Base;
using Xunit;

namespace Authing.ApiClient.Framework.Test.SDKInit
{
    public class InitSDKTest : TestBase
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = await ManagementClient.InitManagementClient(UserPoolId, Secret);
            Assert.NotEmpty(client.AccessToken);
            Assert.NotNull(client.Users);
        }

        [Fact]
        public async void should_init_authing_sdk_with_init_option()
        {
            var client = await ManagementClient.InitManagementClient(init: opt =>
            {
                opt.UserPoolId = UserPoolId;
                opt.Secret = Secret;
            });
            Assert.NotEmpty(client.AccessToken);
            Assert.NotNull(client.Users);
        }
        
        [Fact]
        public async void should_init_authing_authentication_sdk_with_init_option()
        {
            var authenticationClient = new AuthenticationClient(
                opt => { opt.AppId = AppId; }
            );
            var res = await authenticationClient.CheckLoginStatus();
            Assert.Equal(2206, res.Code);
            Assert.Equal(false, res.Status);
            Assert.Equal("登录信息已过期", res.Message);
        }
    }
}