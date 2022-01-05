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
            Assert.Equal(result.Name, "qitao");
        }

        [Fact]
        public async void Users_Detail()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Detail(user.Id, true);
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
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_Exists()
        {
            var client = managementClient;
            var result = await client.Users.Exists(new Types.ExistsOption() {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("result", result);
            Assert.True(result);
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

        [Fact]
        public async void Users_Search()
        {
            var client = managementClient;
            var result = await client.Users.Search("qitao");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_RefreshToken()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.RefreshToken(user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Users_ListGroups()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListGroups(user.Id);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_AddGroup()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.AddGroup(user.Id, "testgroup_Add");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_RemoveGroup()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.RemoveGroup(user.Id, "testgroup_Add");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_ListRoles()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListRoles(user.Id);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_AddRoles()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.AddRoles(user.Id, new List<string>() { "test" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_RemoveRoles()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.RemoveRoles(user.Id, new List<string>() { "test" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_listOrgs()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListOrgs(user.Id);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_listDepartment()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListDepartment(user.Id);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_listAuthorizedResources()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListAuthorizedResources(user.Id, "");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_getUdfValue()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.GetUdfValue(user.Id);
            Assert.NotNull(result.Count);
        }

        [Fact]
        public async void Users_getUdfValueBatch()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.GetUdfValueBatch(new string[] { user.Id });
            Assert.NotNull(result);
        }

        [Fact]
        public async void Users_SetUdfValue()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var udf = new Types.KeyValueDictionary();
            udf.Add("asdad", "val1");
            var result = await client.Users.SetUdfValue(user.Id, udf);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Users_SetUdfValueBatch()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var udf = new Types.KeyValueDictionary();
            udf.Add("asdad", "val1");
            var udfBatch = new Domain.Model.Management.Udf.SetUserUdfValueBatchParam() {
                UserId = user.Id,
                Data = udf
            };
            var result = await client.Users.SetUdfValueBatch(new Domain.Model.Management.Udf.SetUserUdfValueBatchParam[] { udfBatch });
            Assert.True(true);
        }

        [Fact]
        public async void Users_RemoveUdfValue()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.RemoveUdfValue(user.Id, "asdad");
            Assert.True(true);
        }

        [Fact]
        public async void Users_hasRole()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.hasRole(user.Id, "test");
            Assert.True(result);
        }

        [Fact]
        public async void Users_Kick()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Phone = "17620671314"
            });
            var result = await client.Users.Kick(new string[] { user.Id });
            Assert.Equal(result.Code, 200);
        }


        [Fact]
        public async void Users_Logout()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.Logout(new LogoutParam() {
                AppId = "6195ebcf5255f3d735ba9063",
                UserId = user.Id
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_CheckLoginStatus()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.CheckLoginStatus(user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Users_ListUserActions()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.ListUserActions();
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_SendFirstLoginVerifyEmail()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            var result = await client.Users.SendFirstLoginVerifyEmail(new SendFirstLoginVerifyEmailParam(user.Id, "6195ebcf5255f3d735ba9063"));
            Assert.Equal(result.Result.Code, 200);
        }
    }
}
