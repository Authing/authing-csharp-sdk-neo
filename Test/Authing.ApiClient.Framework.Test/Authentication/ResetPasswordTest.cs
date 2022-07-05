using Authing.Library.Domain.Model.Exceptions;
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.ResetPasswordByPhoneCode("13348926753", "9438", "12345678",authingErrorBox);

            Assert.True(result.Code == 200);
        }

        [Fact]
        public async void ResetPasswordByEmailCode_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

           var msg= await client.SendEmail("635877990qq.com", Types.EmailScene.RESET_PASSWORD,authingErrorBox);

            var result = await client.ResetPasswordByEmailCode("635877990@qq.com", "7956", "12345678",authingErrorBox);

            Assert.True(result.Code == 200);
        }
    }
}