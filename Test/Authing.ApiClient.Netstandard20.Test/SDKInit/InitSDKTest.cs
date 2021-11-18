using Authing.ApiClient.Auth;
using Authing.ApiClient.Mgmt;
using Authing.ApiClient.Test.Base;
using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.SDKInit
{
    public class Class1 : TestBase
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = await ManagementClient.InitManagementClient(UserPoolId, Secret);
            Assert.NotEmpty(client.AccessToken);
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
            var client = await ManagementClient.InitManagementClient(init: opt =>
            {
                opt.UserPoolId = UserPoolId;
                opt.Secret = Secret;
            });
            Assert.NotEmpty(client.AccessToken);
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
            var authenticationClient = new AuthenticationClient(
                opt => { opt.AppId = AppId; }
            );
            var res = await authenticationClient.CheckLoginStatus();
            Assert.Equal(2206, res.Code);
            Assert.Equal(false, res.Status);
            Assert.Equal("登录信息已过期", res.Message);
            // authenticationClient.RegisterByEmail(string email, string password, RegisterProfile profile = null, RegisterAndLoginOptions options = null)
        }
    }
}