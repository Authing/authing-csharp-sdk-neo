using System;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.Users
{
    public class user_register : BaseTest
    {
        [Fact]
        public async void should_register_user_by_email()
        {
            // var email = new Random().Next(100000, 999999) + "test@test.com";
            // var res = await authenticationClient.RegisterByEmail(email + "test@test.com", "123456", null,
            //     null);
            // Assert.NotNull(res);
            // Assert.Equal(email, res.Email);
        }
    }
}