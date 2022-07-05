using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class UserTest : BaseTest
    {
        [Fact]
        public async void ListUser_Test()
        {
            var client = managementClient;

            string roleCode = "ListUserRole";

            await client.Roles.Create(roleCode);

            List<string> userIds = new List<string>();
            userIds.Add("61a7274c582843df40616620");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var message = await client.Roles.AddUsers(roleCode, userIds,authingErrorBox);

            Assert.True(message.Code == 200);

            var userList = await client.Roles.ListUsers(roleCode);

            Assert.True(userList.TotalCount == 1 && userList.List.Count() == 1);
        }

        [Fact]
        public async void ListUseWithNameSpacer_Test()
        {
            var client = managementClient;

            string roleCode = "ListUserRole";

            string nameSpace = "613189b38b6c66cac1d211bd";

            await client.Roles.Create(roleCode, null, null, nameSpace);

            List<string> userIds = new List<string>();
            userIds.Add("61a7274c582843df40616620");

            var message = await client.Roles.AddUsers(roleCode, userIds, nameSpace);

            Assert.True(message.Code == 200);

            var userList = await client.Roles.ListUsers(roleCode, new Domain.Model.Management.ListUsersOption { NameSpace = nameSpace });

            Assert.True(userList.TotalCount == 1 && userList.List.Count() == 1);
        }

        [Fact]
        public async void RemoveUser_Test()
        {
            var client = managementClient;

            string roleCode = "ListUserRole";

            string nameSpace = "613189b38b6c66cac1d211bd";

            await client.Roles.Create(roleCode);

            var result = await client.Roles.FindByCode(roleCode);

            Assert.True(result.Code == roleCode);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.Roles.Delete(roleCode,authingErrorBox);

            Assert.True(msg.Code == 200);
        }

        [Fact]
        public async void RemoveUserWithNameSpace_Test()
        {
            var client = managementClient;

            string roleCode = "ListUserRole";

            string nameSpace = "613189b38b6c66cac1d211bd";

            await client.Roles.Create(roleCode, null, null, nameSpace);

            var result = await client.Roles.FindByCode(roleCode, nameSpace);

            Assert.True(result.Code == roleCode);

            var msg = await client.Roles.Delete(roleCode, nameSpace);

            Assert.True(msg.Code == 200);
        }
    }
}