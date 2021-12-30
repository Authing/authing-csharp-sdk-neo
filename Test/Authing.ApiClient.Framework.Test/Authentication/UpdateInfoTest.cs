using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class UpdateInfoTest : BaseTest
    {
        [Fact]
        public async void UpdateProfile_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByPhonePassword("13348926753", "3866364", null);

            var result = await client.UpdateProfile(new Domain.Model.UpdateUserInput { Name = "test" });

            Assert.True(result.Name == "test");
        }

        [Fact]
        public async void UpdatePassword_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByPhonePassword("13348926753", "12345678", null);

            var result = await client.UpdatePassword("3866364", "12345678");

            Assert.NotNull(result);
        }

        [Fact]
        public async void UpdatePhone_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByEmail("635877990@qq.com", "12345678", null);

            await client.SendSmsCode("13348926753");

            var result = await client.UpdatePhone("13348926753", "5570");

            Assert.NotNull(result);
        }

        [Fact]
        public async void RefreshToken_Test()
        {
            var client = authenticationClient;


            var user = await client.LoginByEmail("635877990@qq.com", "12345678", null);


            string oldToken = client.AccessToken;

            var token = await client.RefreshToken();

            Assert.False(oldToken == token.Token);
        }

        [Fact]
        public async void BindPhone_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByEmail("635877990@qq.com", "12345678", null);

            await client.SendSmsCode("13348926753");

            var result = await client.BindPhone("13348926753", "1949");

            Assert.NotNull(result);
        }

        [Fact]
        public async void UnBindPhone_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByEmail("635877990@qq.com", "12345678", null);

            //await client.SendSmsCode("13348926753");

            var result = await client.UnbindPhone();

            Assert.True(string.IsNullOrEmpty(result.Phone));
        }


        [Fact]
        public async void BindEmail_Test()
        {
            var client = authenticationClient;
            //var user = await client.RegisterByUsername("qidong5566", "12345678",null,null);

            await client.LoginByUsername("qidong5566", "12345678", null);
            //await client.SendSmsCode("13348926753");
            await client.SendEmail("2481452007@qq.com", Types.EmailScene.RESET_PASSWORD);

            var result = await client.BindEmail("2481452007@qq.com", "6743");

            Assert.True(string.IsNullOrEmpty(result.Email)==false);
        }

        [Fact]
        public async void UnbindEmail_Text()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.UnbindEmail();

            Assert.True(string.IsNullOrEmpty(result.Email));
        }
    }
}
