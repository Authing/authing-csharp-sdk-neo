using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class MemebersTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void AddMemebers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add(TestUserId);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var res = await client.Orgs.SearchNodes("Tommy");

            var result = await client.Orgs.AddMembers(res.First().Id, userList,authingErrorBox);

            Assert.NotNull(result.Users);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void MoveMembers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add(TestUserId);

            var res = await client.Orgs.SearchNodes("Tommy");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Orgs.MoveMembers(res.First().Id, "62a95f2be36aead9f9b0e002", userList,authingErrorBox);

            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void SetMainDepartment_Test()
        {
            var client = managementClient;

            string userId = TestUserId;

            var res = await client.Orgs.SearchNodes("Tommy");

            var result = await client.Orgs.SetMainDepartment(userId, res.First().Id);

            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void ListMemeber_Test()
        {
            var client = managementClient;

            var res = await client.Orgs.SearchNodes("Tommy");

            string departmentId = res.First().Id;

            var result = await client.Orgs.ListMembers(departmentId, new Domain.Model.Management.Orgs.NodeByIdWithMembersParam(departmentId) { });

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void RemoveMemebers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add(TestUserId);

            var res = await client.Orgs.SearchNodes("Tommy");

            string departmentId = res.First().Id;

            var result = await client.Orgs.RemoveMembers(departmentId, userList);

            Assert.NotNull(result.TotalCount == 0);
        }
    }
}