using Authing.ApiClient.Domain.Client;
using Xunit;

namespace Authing.ApiClient.Netstandard20_up.Test.SDKInit
{
    public class Class1
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = await ManagementClient.InitManagementClient("61797ac183d49f46bcd3574a", "c9dbab9f4dd0547768a58c5c8a5a83ea");
            Assert.NotEmpty(client.AccessToken); 
            Assert.NotNull(client.Users);
        }
        
        [Fact]
        public async void should_init_authing_sdk_with_init_option()
        {
            var client = await ManagementClient.InitManagementClient(init: opt =>
            {
                opt.UserPoolId = "61797ac183d49f46bcd3574a";
                opt.Secret = "c9dbab9f4dd0547768a58c5c8a5a83ea";
            });
            Assert.NotEmpty(client.AccessToken); 
            Assert.NotNull(client.Users);
        }
        
    }
}
