using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class UpdateInfoTest : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UpdateProfile_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("testtmgg", "88886666", null);

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

            var result = await client.UpdateProfile(new Domain.Model.UpdateUserInput
            {
                Name = "测试1",
                Username = "testtmgg",
                Nickname = "tommy",
                Gender = "M",
                Birthdate = "1995/06/29",
                Country = "China",
                Company = "Authing",
                City = "Chengdu",
                Province = "Sichuan",
                PostalCode = "610000"
            },authingErrorBox);

            Assert.True(result.Name == "tommy");
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UpdatePassword_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdatePassword("88886666", "88886666",authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UpdatePhone_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("tmgg", "88886666", null);

            await client.SendSmsCode("17665662048");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdatePhone("17665662048", "4002",authingErrorBox: authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UpdateEmail_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("tmgg", "88886666", null);

            await client.SendEmail("574378328@qq.com",Types.EmailScene.CHANGE_EMAIL);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdateEmail("574378328@qq.com", "1226", authingErrorBox: authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RefreshToken_Test()
        {
            var client = authenticationClient;


            var user = await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            string oldToken = client.AccessToken;

            var token = await client.RefreshToken(authingErrorBox);

            Assert.False(oldToken == token.Token);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void BindPhone_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("tmgg", "88886666", null);

            await client.SendSmsCode("17665662048");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.BindPhone("17665662048", "9272",authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UnBindPhone_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByUsername("tmgg", "88886666", null);
            //await client.SendSmsCode("13348926753");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UnbindPhone(authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Phone));
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void BindEmail_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);
   
            await client.SendEmail("574378328@qq.com", Types.EmailScene.VERIFY_EMAIL);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.BindEmail("574378328@qq.com", "6743",authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Email) == false);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void UnbindEmail_Text()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UnbindEmail(authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Email));
        }
    }
}