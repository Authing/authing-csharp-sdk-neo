using System;
using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
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
            //var authenticationClient = new AuthenticationClient(
            //    opt =>
            //    {
            //        opt.AppId = AppId;
            //        opt.Host = Host;
            //    }
            //);
            //var email = "test" + new Random().Next(100000, 999999) + "@test.com";
            //var res = await authenticationClient.RegisterByEmail(email, "123456", null,
            //    null);
            //Assert.NotNull(res);
            //Assert.Equal(email, res.Email);
        }
    }
}