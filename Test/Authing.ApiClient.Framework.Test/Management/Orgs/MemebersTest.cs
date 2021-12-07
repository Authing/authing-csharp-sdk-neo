using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class MemebersTest : BaseTest
    {
        [Fact]
        public async void AddMemebers_Test()
        {
            var client = managementClient;

            string userId = "61a7274c582843df40616620 ";

            List<string> userList = new List<string>();
            userList.Add("61a7274c582843df40616620");

            var result = await client.Orgs.AddMembers("61af013d691c6cd83c4a8ac4", userList);

            Assert.NotNull(result.Users);
        }

        [Fact]
        public async void MoveMembers_Test()
        {
            var client = managementClient;

            List<string> userList = new List<string>();
            userList.Add("61a7274c582843df40616620");


            var result = await client.Orgs.MoveMembers("61af013d691c6cd83c4a8ac4", "61af013de626c68c3f1369d8", userList);

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
