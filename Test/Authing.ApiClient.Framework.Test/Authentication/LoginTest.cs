﻿using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class LoginTest : BaseTest
    {
        [Fact]
        public async void LoginByEmail_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByEmail("635877990@qq.com", "3866364", null);
            Assert.NotNull(result);
        }

        [Fact]
        public async void LoginByUserName_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByUsername("qidong2333", "Qd3866364,,..", null);

            //client.SetCurrentUser(result);

            //client.SetCurrentUser(null);

            //client.CheckLoggedIn();

            Assert.NotNull(result);
        }

        [Fact]
        public async void LoginByPhoneCode_Test()
        {
            var client = authenticationClient;

            var res = await client.SendSmsCode("17665662048");

            var result = await client.LoginByPhoneCode("13348926753", "2950", null);

            Assert.NotNull(result);
        }

        [Fact]
        public async void LoginByPhonePassword_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByUsername("qidong5566", "12345678", null);

            var loginStatus = await client.CheckLoginStatus(client.AccessToken);

            Assert.True(loginStatus.Status);

            Assert.NotNull(result);
        }

        [Fact]
        public async void SetCurrentUser_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByPhonePassword("13348926753", "12345678", null);

            client.SetCurrentUser(result);

            Assert.NotNull(client.GetCurrentUser());
        }

        [Fact]
        public async void SetToken_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByPhonePassword("13348926753", "3866364", null);

            client.SetToken(result.Token);

            Assert.True(client.AccessToken == result.Token);
        }

        [Fact]
        public async void CheckPasswordStrength_Test()
        {
            var client = authenticationClient;

            var result = await client.CheckPasswordStrength("3866364");

            Assert.True(result.Valid);
        }

        [Fact]
        public async void LoginByAd_Test()
        {
            var user = await authenticationClient.LoginByAd("Administrator", "19950630@tm");
            Assert.NotNull(user);
        }
    }
}