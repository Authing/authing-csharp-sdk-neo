using System;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.Users
{
    public class user_register : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void should_register_user_by_email()
        {
            var email = new Random().Next(100000, 999999) + "test@test.com";
            var res = await authenticationClient.RegisterByEmail(email, "123456", null,
                null);
            Assert.NotNull(res);
            Assert.Equal(email, res.Email);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void Login_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByPhonePassword("17665662048", "88886666", null);
        }
    }
}