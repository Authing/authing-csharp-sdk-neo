using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Test.Base;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.Users
{
    public class user_register : TestBase
    {
        [Fact]
        public async void should_register_user_by_email()
        {
            var authenticationClient = new AuthenticationClient(
                opt => { opt.AppId = AppId; }
            );
            // authenticationClient.RegisterByEmail(string email, string password, RegisterProfile profile = null,
            //     RegisterAndLoginOptions options = null)
        }
    }
}