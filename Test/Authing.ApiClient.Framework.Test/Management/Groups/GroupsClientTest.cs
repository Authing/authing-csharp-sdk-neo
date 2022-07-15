using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Groups
{
    public class GroupsClientTest : BaseTest
    {
        [Fact]
        public async Task Groups_List()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

           var  createresult= await managementClient.Groups.Create("testgroup", "testgroup",authingErrorBox:authingErrorBox);
            var list = await managementClient.Groups.List();
            Assert.NotEmpty(list.List);

            var result = await managementClient.Groups.Delete("testgroup",authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Groups_Update()
        {
            var list = await managementClient.Groups.List();
            Assert.NotEmpty(list.List);
            var group = list.List.First();
            group.Description = "测试描述";

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            await managementClient.Groups.Update(group.Code, group.Name, group.Description,authingErrorBox:authingErrorBox);

            await managementClient.Groups.Update("123123213", group.Name, group.Description, authingErrorBox: authingErrorBox);

            list = await managementClient.Groups.List();
            Assert.NotEmpty(list.List);
            group = list.List.First();
            Assert.Equal(group.Description, "测试描述");
        }

        [Fact]
        public async Task Groups_Add()
        {
            await managementClient.Groups.Create("testgroup_Add", "testgroup_Add");
            var list = await managementClient.Groups.List();
            Assert.NotEmpty(list.List);
        }

        [Fact]
        public async Task Groups_Delete()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Groups.Delete("testgroup",authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Groups_Detail()
        {
            await managementClient.Groups.Create("testgroup_Detail", "testgroup_Detail", "Detail");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var group = await managementClient.Groups.Detail("testgroup_Detail",authingErrorBox);
            Assert.Equal(group.Description, "Detail");
            await managementClient.Groups.Delete("testgroup_Detail");
        }

        [Fact]
        public async Task Groups_DeleteMany()
        {
            await managementClient.Groups.Create("testgroup_DeleteMany1", "testgroup_DeleteMany1", "testgroup_DeleteMany1");
            await managementClient.Groups.Create("testgroup_DeleteMany2", "testgroup_DeleteMany2", "testgroup_DeleteMany2");
            await managementClient.Groups.Create("testgroup_DeleteMany3", "testgroup_DeleteMany3", "testgroup_DeleteMany3");
            var list = await managementClient.Groups.List();
            Assert.Equal(list.TotalCount, 4);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Groups.DeleteMany(new List<string>()
                { "testgroup_DeleteMany1", "testgroup_DeleteMany2", "testgroup_DeleteMany3" },authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Groups_ListUsers()
        {
            //TODO:返回的用户列表不包含目标用户,
            /*
             * {"data":{"role":null}}
             */

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var temp = await managementClient.Groups.Create("testgroup_ListUsers", "testgroup_ListUsers", "testgroup_ListUsers");
            await managementClient.Groups.AddUsers("testgroup_ListUsers", new List<string>() { TestUserId },authingErrorBox);
            var users = await managementClient.Groups.ListUsers("testgroup_ListUsers", new ListUsersOption() { NameSpace = temp.Code },authingErrorBox);
            Assert.NotEmpty(users.List); ;
            Assert.Equal(users.List.Take(1).First().Id, TestUserId);
        }

        [Fact]
        public async Task Groups_AddUser()
        {
            await managementClient.Groups.Create("testgroup_AddUser", "testgroup_AddUser", "testgroup_AddUser");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Groups.AddUsers("testgroup_AddUser", new List<string>() { TestUserId },authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Groups_RemoveUsers()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Groups.RemoveUsers("testgroup_AddUser", new List<string>() { TestUserId }, authingErrorBox);
            Assert.Equal(result.Code, 200);
            await managementClient.Groups.Delete("testgroup_AddUser");
        }

        [Fact]
        public async Task Groups_ListAuthorizedResources()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Groups.ListAuthorizedResources("ListAuthorizedResources", "6172807001258f603126a78a", ResourceType.DATA,authingErrorBox);
            Assert.NotEmpty(result.List);
            Assert.Equal(result.List.First().Code, "Books:*");
        }
    }
}