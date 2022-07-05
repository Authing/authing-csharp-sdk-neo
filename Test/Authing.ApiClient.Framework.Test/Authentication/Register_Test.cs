using Xunit;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class Register_Test : BaseTest
    {
        [Fact]
        public async void RegisterByEmail()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

            var result = await client.RegisterByEmail("qidong5566@outlook.com", "3866364", null, null,authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void RegisterByUserName()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.RegisterByUsername("qidong2333", "Qd3866364,,..",null, null,authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void RegisterByPhoneCode()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.SendSmsCode("13348926753",authingErrorBox);

            Assert.True(msg.Code == 200);

            Library.Domain.Model.Exceptions.AuthingErrorBox error = new Library.Domain.Model.Exceptions.AuthingErrorBox();

            var result = await client.RegisterByPhoneCode("13348926753", "7009", "3866364", null, null,error);

            Assert.NotNull(result);
        }
    }
}