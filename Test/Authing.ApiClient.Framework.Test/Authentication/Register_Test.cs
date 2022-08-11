using Xunit;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class Register_Test : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RegisterByEmail()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

            var result = await client.RegisterByEmail("qidong5566@outlook.com", "3866364", null, null,authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RegisterByUserName()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.RegisterByUsername("test@test.com", "1122334455",null, null,authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RegisterByPhoneCode()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.SendSmsCode("17620671314",authingErrorBox);

            Assert.True(msg.Code == 200);

            Library.Domain.Model.Exceptions.AuthingErrorBox error = new Library.Domain.Model.Exceptions.AuthingErrorBox();

            var result = await client.RegisterByPhoneCode("17620671314", "9803", "88886666", null, null,error);

            Assert.NotNull(result);
        }
    }
}