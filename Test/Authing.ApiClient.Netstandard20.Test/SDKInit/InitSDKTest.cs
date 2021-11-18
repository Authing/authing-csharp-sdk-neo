using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.SDKInit
{
    public class Class1 : BaseTest
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = managementClient = await GetManagementClient();
            Assert.NotNull(client.Users);
            Assert.NotNull(client.Roles);
            Assert.NotNull(client.Acl);
            Assert.NotNull(client.Groups);
            Assert.NotNull(client.Orgs);
            Assert.NotNull(client.Udf);
            Assert.NotNull(client.Whitelist);
            Assert.NotNull(client.Userpool);
            Assert.NotNull(client.Policies);
        }

        [Fact]
        public async void should_init_authing_sdk_with_init_option()
        {
            var client = managementClient = await GetManagementClient();
            Assert.NotNull(client.Users);
            Assert.NotNull(client.Roles);
            Assert.NotNull(client.Acl);
            Assert.NotNull(client.Groups);
            Assert.NotNull(client.Orgs);
            Assert.NotNull(client.Udf);
            Assert.NotNull(client.Whitelist);
            Assert.NotNull(client.Userpool);
            Assert.NotNull(client.Policies);
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