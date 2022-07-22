using Authing.ApiClient.Domain.Model;
using Authing.Library.Domain.Model.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Userpool
{
    public class UserpoolClientTest : BaseTest
    {
        [Fact]
        public async Task Userpool_Detail()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();
            var result = await managementClient.Userpool.Detail(authingErrorBox);
            Assert.Equal(result.Id, UserPoolId);
        }

        [Fact]
        public async Task Userpool_Update()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();
            var result = await managementClient.Userpool.Detail(authingErrorBox);
            result.Description = "测试描述";
            result = await managementClient.Userpool.Update(new UpdateUserpoolInput() { Description = "测试描述" });
            Assert.Equal(result.Description, "测试描述");
        }

        [Fact]
        public async Task Userpool_ListEnv()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            await managementClient.Userpool.AddEnv("123", "123", authingErrorBox);
            var result = await managementClient.Userpool.ListEnv();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Userpool_AddEnv()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();
            var result = await managementClient.Userpool.AddEnv("123", "123", authingErrorBox);
            Assert.Equal(result, 200);
        }

        [Fact]
        public async Task Userpool_RemoveEnv()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Userpool.RemoveEnv("123", authingErrorBox);
            Assert.Equal(result, 200);
        }
    }
}