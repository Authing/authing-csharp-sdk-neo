using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class ResetPasswordTest : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void ResetPasswordByPhoneCode_Test()
        {
            var client = authenticationClient;

            await client.SendSmsCode("17665662048");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.ResetPasswordByPhoneCode("17665662048", "1149", "12345678",authingErrorBox);

            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void ResetPasswordByEmailCode_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

           var msg= await client.SendEmail("574378328qq.com", Types.EmailScene.RESET_PASSWORD,authingErrorBox);

            var result = await client.ResetPasswordByEmailCode("574378328@qq.com", "7956", "88886666",authingErrorBox);

            Assert.True(result.Code == 200);
        }
    }
}