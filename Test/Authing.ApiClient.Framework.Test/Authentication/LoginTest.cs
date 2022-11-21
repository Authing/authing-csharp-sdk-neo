using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class LoginTest : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void LoginByEmail_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox errorBox = new AuthingErrorBox();

            var result = await client.LoginByEmail("574378328@qq.com", "88886666", null,errorBox);
            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void LoginByUserName_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox error;

            var result = await client.LoginByUsername("qidong11233", "12345678", new RegisterAndLoginOptions { AutoRegister = false },authingErrorBox: error = new AuthingErrorBox());

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void LoginByPhoneCode_Test()
        {
            var client = authenticationClient;

            var res = await client.SendSmsCode("17665662048");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.LoginByPhoneCode("17665662048", "5034", null,authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void LoginByPhonePassword_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.LoginByUsername("tmgg", "88886666", null,authingErrorBox);

            var loginStatus = await client.CheckLoginStatus(client.AccessToken,authingErrorBox);

            Assert.True(loginStatus.Status);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void SetCurrentUser_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByPhonePassword("17665662048", "88886666", null);

            client.SetCurrentUser(result);

            Assert.NotNull(client.GetCurrentUser());
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void SetToken_Test()
        {
            var client = authenticationClient;

            var result = await client.LoginByPhonePassword("17665662048", "88886666", null);

            client.SetToken(result.Token);

            Assert.True(client.AccessToken == result.Token);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void CheckPasswordStrength_Test()
        {
            var client = authenticationClient;

            var result = await client.CheckPasswordStrength("1");

            Assert.True(result.Valid);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void LoginByAd_Test()
        {
            var user = await authenticationClient.LoginByAd("Administrator", "19950630@tm");
            Assert.NotNull(user);
        }
    }
}