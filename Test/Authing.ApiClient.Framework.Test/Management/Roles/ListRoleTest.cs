using Authing.Library.Domain.Model.Exceptions;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class ListRoleTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListRole_Test()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            await client.Roles.Create("createTest", nameSpace: "default", authingErrorBox: authingErrorBox);
            var result = await client.Roles.List();

            Assert.True(result.List.Any());
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListRoleWithNameSpace_Test()
        {
            var client = managementClient;

            string nameSpace = "default";

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Roles.List(nameSpace, authingErrorBox: authingErrorBox);

            Assert.True(result.List.Any());
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListRoleWithNameSpaceAndPage_Test()
        {
            var client = managementClient;

            string nameSpace = "default";

            var result = await client.Roles.List(nameSpace, 1, 5);

            Assert.True(result.List.Any());
        }
    }
}