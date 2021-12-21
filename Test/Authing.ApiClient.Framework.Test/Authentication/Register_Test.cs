using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class Register_Test : BaseTest
    {
        [Fact]
        public async void RegisterByEmail()
        {
            var client = authenticationClient;

            var result = await client.RegisterByEmail("635877990@qq.com", "3866364", null, null);

            Assert.NotNull(result);
        }

        [Fact]
        public async void RegisterByUserName()
        {
            var client = authenticationClient;

            var result = await client.RegisterByUsername("qidong5566", "3866364", null, null);

            Assert.NotNull(result);
        }

        [Fact]
        public async void RegisterByPhoneCode()
        {
            var client = authenticationClient;

            var msg = await client.SendSmsCode("13348926753");

            Assert.True(msg.Code == 200);

            var result = await client.RegisterByPhoneCode("13348926753", "3422", "3866364", null, null);

            Assert.NotNull(result);
        }

        
    }
}
