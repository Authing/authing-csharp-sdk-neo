using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class MemebersTest : BaseTest
    {
        [Fact]
        public async void AddMemebers_Test()
        {
            var client = managementClient;

            string userId = "62c3e974562f434f6fb66bb3 ";

            List<string> userList = new List<string>();
            userList.Add("62c3e974562f434f6fb66bb3");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Orgs.AddMembers("629872091ab96fdcc3904085", userList,authingErrorBox);

            Assert.NotNull(result.Users);
        }

        [Fact]
        public async void MoveMembers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add("61a7274c582843df40616620");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Orgs.MoveMembers("629872091ab96fdcc3904085", "61af013de6c68c3f1369d8", userList,authingErrorBox);

            Assert.True(result.Code == 200);
        }

        [Fact]
        public async void SetMainDepartment_Test()
        {
            var client = managementClient;

            string userId = "61a7274c582843df40616620 ";

            string departmentId = "61af013de626c68c3f1369d8";

            var result = await client.Orgs.SetMainDepartment(userId, departmentId);

            Assert.True(result.Code == 200);
        }

        [Fact]
        public async void ListMemeber_Test()
        {
            var client = managementClient;

            string departmentId = "61af013d691c6cd83c4a8ac4";

            var result = await client.Orgs.ListMembers(departmentId, new Domain.Model.Management.Orgs.NodeByIdWithMembersParam(departmentId) { });

            Assert.NotNull(result);
        }

        [Fact]
        public async void RemoveMemebers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add("61a7274c582843df40616620");

            string departmentId = "61af013d691c6cd83c4a8ac4";

            var result = await client.Orgs.RemoveMembers(departmentId, userList);

            Assert.NotNull(result.TotalCount == 0);
        }
    }
}