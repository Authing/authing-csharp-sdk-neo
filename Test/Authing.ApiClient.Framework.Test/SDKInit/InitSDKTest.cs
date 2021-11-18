using Authing.ApiClient.Domain.Client;
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
    }
}