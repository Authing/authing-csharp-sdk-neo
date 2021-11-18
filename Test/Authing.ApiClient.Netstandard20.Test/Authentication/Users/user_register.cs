using Authing.ApiClient.Auth;
using Authing.ApiClient.Auth.Types;
using Authing.ApiClient.Test.Base;
using Authing.ApiClient.Types;
using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.Authentication.Users
{
    public class user_register : TestBase
    {
        [Fact]
        public async void should_register_user_by_email()
        {
            RegisterProfile profile = null;
            RegisterAndLoginOptions registerAndLoginOptions = null;
            var authenticationClient = new AuthenticationClient(
                opt => { opt.AppId = AppId; }
            );
            var res = await authenticationClient.RegisterByEmail("test@test.com", "123456", profile, registerAndLoginOptions);
            Assert.NotNull(res);
        }
    }
}