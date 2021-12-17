using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Users;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Users
{
    public class ManagementClientUserTest : BaseTest
    {
        //[Fact]
        //public async void Users_Create()
        //{
        //    var result = await managementClient.Users.Create(new CreateUserInput() {
        //        Email = "qitaotest@authing.cn",
        //        Password = "123456",
        //    });
        //    Assert.Equal(result.Email, "qitaotest@authing.cn");
        //}

        [Fact]
        public async void Users_Update()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption() {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.NotNull(user);
            var result = await client.Users.Update(user.Id,new UpdateUserInput() {
                Name = "qitao"
            });
            Console.WriteLine("result", result);
            Assert.NotNull(result);
            //Assert.Equal(result.Name, "qitao");
        }

        [Fact]
        public async void Users_Detail()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Detail(user.Id, true);
            Console.WriteLine("result", result);
            Assert.Equal(result.Email, "qitaotest@authing.cn");
        }

        [Fact]
        public async void Users_Delete()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest2@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest2@authing.cn");
            var result = await client.Users.Delete(user.Id);
            Console.WriteLine("result", result);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_DeleteMany()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest3@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest3@authing.cn");
            var result = await client.Users.DeleteMany(new List<string>() { user.Id });
            Console.WriteLine("result", result);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_Batch()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Batch(new List<string>() { user.Id });
            Console.WriteLine("result", result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Users_List()
        {
            var client = managementClient;
            var result = await client.Users.List(1, 10);
            Console.WriteLine("result", result);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_ListArchivedUsers()
        {
            var client = managementClient;
            var result = await client.Users.ListArchivedUsers(1, 10);
            Console.WriteLine("result", result);
            Assert.Equal(result.TotalCount, 0);
        }

        [Fact]
        public async void Users_Exists()
        {
            var client = managementClient;
            var result = await client.Users.Exists(new Types.ExistsOption() {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("result", result);
            Assert.Equal(result, true);
        }

        [Fact]
        public async void Users_Find()
        {
            var client = managementClient;
            var result = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("result", result);
            Assert.Equal(result.Email, "qitaotest@authing.cn");
        }
    }
}
