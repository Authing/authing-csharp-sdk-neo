using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class UpdateInfoTest : BaseTest
    {
        [Fact]
        public async void UpdateProfile_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

            var result = await client.UpdateProfile(new Domain.Model.UpdateUserInput { Name = "test" },authingErrorBox);

            Assert.True(result.Name == "test");
        }

        [Fact]
        public async void UpdatePassword_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdatePassword("3866364", "1234567228",authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void UpdatePhone_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            await client.SendSmsCode("13348926753");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdatePhone("13348926753", "5570",authingErrorBox: authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void UpdateEmail_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            await client.SendEmail("qidong5566@outlook.com",Types.EmailScene.CHANGE_EMAIL);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UpdateEmail("qidong5566@outlook.com", "5570", authingErrorBox: authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void RefreshToken_Test()
        {
            var client = authenticationClient;


            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            string oldToken = client.AccessToken;

            var token = await client.RefreshToken(authingErrorBox);

            Assert.False(oldToken == token.Token);
        }

        [Fact]
        public async void BindPhone_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            await client.SendSmsCode("13348926753");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.BindPhone("13348926753", "1949",authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void UnBindPhone_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByUsername("qidong5566", "12345678", null);
            //await client.SendSmsCode("13348926753");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UnbindPhone(authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Phone));
        }

        [Fact]
        public async void BindEmail_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);
   
            await client.SendEmail("2481452007@qq.com", Types.EmailScene.VERIFY_EMAIL);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.BindEmail("2481452007@qq.com", "6743",authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Email) == false);
        }

        [Fact]
        public async void UnbindEmail_Text()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.UnbindEmail(authingErrorBox);

            Assert.True(string.IsNullOrEmpty(result.Email));
        }
    }
}