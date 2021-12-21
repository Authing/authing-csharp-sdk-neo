using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class ResetPasswordTest : BaseTest
    {
        [Fact]
        public async void ResetPasswordByPhoneCode_Test()
        {
            var client = authenticationClient;
           
            await client.SendSmsCode("13348926753");

            var result = await client.ResetPasswordByPhoneCode("13348926753", "9438", "12345678");

            Assert.True(result.Code == 200);

        }

        [Fact]
        public async void ResetPasswordByEmailCode_Test()
        {
            var client = authenticationClient;

            await client.SendEmail("635877990@qq.com", Types.EmailScene.RESET_PASSWORD);

           var result= await client.ResetPasswordByEmailCode("635877990@qq.com", "7956", "12345678");

            Assert.True(result.Code == 200);
        }

    }
}
