using Xunit;

namespace Authing.ApiClient.Netstandard20_up.Test.SDKInit
{
    public class Class1 : BaseTest
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
    }
}