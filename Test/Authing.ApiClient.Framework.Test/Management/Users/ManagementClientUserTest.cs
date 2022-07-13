using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Users;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Users
{
    public class ManagementClientUserTest : BaseTest
    {
        [Fact]
        public async void Users_Create()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Users.Create(new CreateUserInput()
            {
                Email = "qitaotest@authing.cn",
                Password = "123456",
            },null,authingErrorBox);
            Assert.Equal(result.Email, "qitaotest@authing.cn");
        }

        [Fact]
        public async void Users_Update()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            },authingErrorBox:authingErrorBox);
            Console.WriteLine("user", user);
            Assert.NotNull(user);
            var result = await client.Users.Update(user.Id, new UpdateUserInput()
            {
                Name = "qitao"
            },authingErrorBox:authingErrorBox);
            Console.WriteLine("result", result);
            Assert.Equal(result.Name, "qitao");
        }

        [Fact]
        public async void Users_Detail()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Detail(user.Id, true, authingErrorBox: authingErrorBox);
            Assert.Equal(result.Email, "qitaotest@authing.cn");
        }

        [Fact]
        public async void Users_Delete()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Delete(user.Id,authingErrorBox);
            Console.WriteLine("result", result);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_DeleteMany()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.DeleteMany(new List<string>() { user.Id },authingErrorBox);
            Console.WriteLine("result", result);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_Batch()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });
            Console.WriteLine("user", user);
            Assert.Equal(user.Email, "qitaotest@authing.cn");
            var result = await client.Users.Batch(new List<string>() { user.Id },authingErrorBox: authingErrorBox);
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
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var result = await client.Users.ListArchivedUsers(1, 10,authingErrorBox);
            Console.WriteLine("result", result);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Users_Exists()
        {
            var client = managementClient;
            var result = await client.Users.Exists(new Types.ExistsOption()
            {
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.AddGroup(user.Id, "testgroup_Add",authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.RemoveGroup(user.Id, "testgroup_Add",authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.ListRoles(user.Id,authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.AddRoles(user.Id, new List<string>() { "test" },authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.RemoveRoles(user.Id, new List<string>() { "test" },authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.ListOrgs(user.Id,authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.ListDepartment(user.Id,authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.ListAuthorizedResources(user.Id, "",authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.GetUdfValue(user.Id,authingErrorBox);
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
            var udfBatch = new Domain.Model.Management.Udf.SetUserUdfValueBatchParam()
            {
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.hasRole(user.Id, "test",authingErrorBox:authingErrorBox);
            Assert.True(result);
        }

        [Fact]
        public async void Users_Kick()
        {
            var client = managementClient;
            var user = await client.Users.Find(new FindUserOption()
            {
                Email = "qitaotest@authing.cn"
            });

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.Kick(new string[] { user.Id },authingErrorBox);
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

            AuthingErrorBox authingErrorBox=new AuthingErrorBox();

            var result = await client.Users.Logout(new LogoutParam()
            {
                AppId = "6195ebcf5255f3d735ba9063",
                UserId = user.Id
            },authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.CheckLoginStatus(user.Id,authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.ListUserActions(authingErrorBox:authingErrorBox);
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

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.SendFirstLoginVerifyEmail(new SendFirstLoginVerifyEmailParam(user.Id, "6195ebcf5255f3d735ba9063"),authingErrorBox);
            Assert.Equal(result.Result.Code, 200);
        }

        [Fact]
        public async void Users_CreateUsers()
        {
            var client = managementClient;
            var userList = new List<CreateUserInput>() {
                new CreateUserInput(){ Email = "qitaoVT4@authing.cn",Password = "123456" },
                new CreateUserInput(){ Email = "qitaoVT5@authing.cn",Password = "123456" }
            };
            var result = await client.Users.CreateUsers(userList);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_GetUserTenants()
        {
            var client = managementClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Users.GetUserTenants("61c560fc3e85f6d56bc6aa77",authingErrorBox);
            Assert.NotEmpty(result.Tenants);
        }

        [Fact]
        public async void Users_LinkIdentity()
        {
            var client = managementClient;
            var result = await client.Users.LinkIdentity(new LinkIdentityOption()
            {
                UserId = "61c560fc3e85f6d56bc6aa77",
                UserIdInIdp = "12345",
                Identifier = "github",
                IsSocial = true,
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Users_UnlinkIdentity()
        {
            var client = managementClient;
            var result = await client.Users.UnlinkIdentity(new UnlinkIdentityOption()
            {
                UserId = "61c560fc3e85f6d56bc6aa77",
                Identifier = "github",
                IsSocial = true,
            });
            Assert.Equal(result.Code, 200);
        }
    }
}