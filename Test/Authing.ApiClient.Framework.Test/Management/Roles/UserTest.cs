using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class UserTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void ListUser_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            await client.Roles.Create(roleCode);

            List<string> userIds = new List<string>();
            userIds.Add(TestUserId);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var message = await client.Roles.AddUsers(roleCode, userIds,"default",authingErrorBox);

            Assert.True(message.Code == 200);

            var userList = await client.Roles.ListUsers(roleCode);

            Assert.True(userList.TotalCount == 1 && userList.List.Count() == 1);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void ListUseWithNameSpacer_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

            await client.Roles.Create(roleCode, null, null, nameSpace);

            List<string> userIds = new List<string>();
            userIds.Add(TestUserId);

            var message = await client.Roles.AddUsers(roleCode, userIds, nameSpace);

            Assert.True(message.Code == 200);

            var userList = await client.Roles.ListUsers(roleCode, new Domain.Model.Management.ListUsersOption { NameSpace = nameSpace });

            Assert.True(userList.TotalCount == 1 && userList.List.Count() == 1);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void RemoveUser_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

            await client.Roles.Create(roleCode);

            var result = await client.Roles.FindByCode(roleCode);

            Assert.True(result.Code == roleCode);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.Roles.Delete(roleCode,"default",authingErrorBox);

            Assert.True(msg.Code == 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void RemoveUserWithNameSpace_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

            await client.Roles.Create(roleCode, null, null, nameSpace);

            var result = await client.Roles.FindByCode(roleCode, nameSpace);

            Assert.True(result.Code == roleCode);

            var msg = await client.Roles.Delete(roleCode, nameSpace);

            Assert.True(msg.Code == 200);
        }
    }
}