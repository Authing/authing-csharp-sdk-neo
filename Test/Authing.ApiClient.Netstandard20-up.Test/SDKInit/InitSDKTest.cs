using Authing.ApiClient.Domain.Client;
using Authing.ApiClient.Test.Base;
using Xunit;
using ManagementClient = Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient.ManagementClient;

namespace Authing.ApiClient.Netstandard20_up.Test.SDKInit
{
    public class Class1 : TestBase
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
