using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Acl
{
    public class AclManagementTest : BaseTest
    {
        [Fact]
        public async Task Acl_CreateNamespace()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.CreateNamespace("testNameSpac22", "testNameSpac22e", "testNameSpace",authingErrorBox);
            Assert.True(result.Code== "testNameSpace");
        }

        [Fact]
        public async Task Acl_ListNamespaces()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.ListNamespaces(5,10,authingErrorBox: authingErrorBox);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_UpdateNamespace()
        {
            var data = await managementClient.Acl.CreateNamespace("testNameSpace2", "testNameSpace2", "testNameSpace2");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.UpdateNamespace(data.Id.ToString(), 
                new UpdateNamespaceParam() { Code = "testNameSpace2", Name = "testNameSpace2", Description = "测试描述" }
                ,authingErrorBox);
            Assert.True(result.Description== "测试描述");
        }

        [Fact]
        public async Task Acl_DeleteNamespace()
        {
            var list = await managementClient.Acl.ListNamespaces();

            int code = list.List.FirstOrDefault(i => i.Code == $"testNameSpace2").Id;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.DeleteNamespace(code,authingErrorBox);
            Assert.True(result.Code==200);
        }

        [Fact]
        public async Task Acl_ListResources()
        {
            try
            {
                AuthingErrorBox authingErrorBox = new AuthingErrorBox();

                var list = await managementClient.Acl.ListResources(new ResourceQueryFilter()
                { NameSpaceCode = "62a9902a80f55c22346eb296", Type = ResourceType.API }, authingErrorBox);
                Assert.NotEmpty(list.List);
            }
            catch (Exception exp)
            { 
            
            }
        }

        [Fact]
        public async Task Acl_CreateResource()
        {
            string code = "";
            var list = await managementClient.Acl.ListNamespaces();
            var id = list.List.FirstOrDefault(i => i.Name == "test")?.Code;


            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.CreateResource(new
                ResourceParam()
            {
                Code = code = new Random().Next().ToString(),
                NameSpace = id,
                Type = ResourceType.DATA,
                //NameSpace = "6172807001258f603126a78a",
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "123", Description = "123" } },
            },authingErrorBox);
            Assert.Equal(result.Code, code);
        }

        [Fact]
        public async Task Acl_GetResourceById()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.GetResourceById("default",authingErrorBox);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_FindResourceByCode()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Acl.FindResourceByCode("113213", "62a9902a80f55c22346eb296", authingErrorBox);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_UpdateResource()
        {
            //var r = await managementClient.acl.FindResourceByCode("Book", "test");
            var result = await managementClient.Acl.UpdateResource("ecs", new ResourceParam() { Code = "Book", NameSpace = "test", Description = "HelloWord", Type = ResourceType.API });
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_DeleteResource()
        {
            string code = "";
            var list = await managementClient.Acl.ListNamespaces();
            var id = list.List.FirstOrDefault(i => i.Code == "test")?.Code;
            var result = await managementClient.Acl.CreateResource(new
                ResourceParam()
            {
                Code = code = new Random().Next().ToString(),
                NameSpace = id,
                Type = ResourceType.DATA,
                //NameSpace = "6172807001258f603126a78a",
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "123", Description = "123" } },
            });
            Assert.Equal(result.Code, code);

            Assert.True(await managementClient.Acl.DeleteResource(code, id.ToString()));
        }

        [Fact]
        public async Task Acl_Allow()
        {
            var result = await managementClient.Acl.Allow(TestUserId, "test", "Cat:*", "Cat:Read");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Acl_RevokeResource()
        {
            //TODO:权限组不存在
            var result = await managementClient.Acl.RevokeResource(
                new RevokeResourceParams()
                {
                    NameSpace = "test",
                    Opts = new List<RevokeResourceOpt>()
                        {new RevokeResourceOpt(){TargetIdentifier = TestUserId,TargetType = PolicyAssignmentTargetType.USER}},
                    Resource = "Cat:*"
                });
        }

        [Fact]
        public async Task Acl_IsAllowed()
        {
            var result = await managementClient.Acl.IsAllowed(TestUserId, "Cat:*", "Cat:Read", "test");
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_ListAuthorizedResources()
        {
            var result = await managementClient.Acl.ListAuthorizedResources(
                PolicyAssignmentTargetType.USER, TestUserId,
                "test",
                new ListAuthorizedResourcesOptions() { ResourceType = ResourceType.DATA });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_GetAuthorizedTargets()
        {
            var result = await managementClient.Acl.GetAuthorizedTargets(new GetAuthorizedTargetsOptions()
            {
                NameSpace = "test",
                Resource = "Cat",
                ResourceType = ResourceType.DATA,
                Actions = new AuthorizedTargetsActionsInput(Operator.OR, new List<string>() { "Read" }),
                TargetType = PolicyAssignmentTargetType.USER,
            });

            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_AuthorizeResource()
        {
            var result = await managementClient.Acl.AuthorizeResource("test", "Cat:*"
                , new List<AuthorizeResourceOpt>()
                {
                    new AuthorizeResourceOpt(PolicyAssignmentTargetType.USER,"61a5c55fc89ff91083293e45",new List<string>(){"Cat:Read"})
                });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Acl_ProgrammaticAccessAccountList()
        {
            //TODO:网页后台存在数据但本地无法获取
            var result = await managementClient.Acl.ProgrammaticAccessAccountList(new ProgrammaticAccessAccountListProps() { AppId = AppId });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_CreateProgrammaticAccessAccount()
        {
            //TODO:{"code":500,"message":"null value in column \"token_lifetime\" violates not-null constraint"}
            var result = await managementClient.Acl.CreateProgrammaticAccessAccount(AppId,
                new CreateProgrammaticAccessAccountParam() { Remarks = "测试创建编程账户", AppId = AppId, Token_lifetime = 600 });
        }

        [Fact]
        public async Task Acl_DeleteProgrammaticAccessAccount()
        {
            //TODO:测试不通过
            var result = await managementClient.Acl.DeleteProgrammaticAccessAccount("123");
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_EnableProgrammaticAccessAccount()
        {
            //TODO:测试不通过
            var result = await managementClient.Acl.EnableProgrammaticAccessAccount("61e52a0b3e3e7b4cae7cab06");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_DisableProgrammaticAccessAccount()
        {
            //TODO:测试不通过
            var result = await managementClient.Acl.DisableProgrammaticAccessAccount("123");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_GetApplicationAccessPolicies()
        {
            var result =
                await managementClient.Acl.GetApplicationAccessPolicies(new AppAccessPolicyQueryFilter()
                { AppId = AppId });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_EnableApplicationAccessPolicy()
        {
            var result = await managementClient.Acl.EnableApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_DisableApplicationAccessPolicy()
        {
            var result = await managementClient.Acl.DisableApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_DeleteApplicationAccessPolicy()
        {
            var result = await managementClient.Acl.DeleteApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_AllowApplicationAccessPolicy()
        {
            var result = await managementClient.Acl.AllowAccessApplication(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_DenyApplicationAccessPolicy()
        {
            var result = await managementClient.Acl.DenyAccessApplication(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_UpdateDefaultApplicationAccessPolicy()
        {
            var result =
                await managementClient.Acl.UpdateDefaultApplicationAccessPolicy(new DefaultAppAccessPolicy()
                {
                    AppId = AppId,
                    DefaultStrategy = DefaultStrategy.ALLOW_ALL,
                });
            Assert.NotNull(result);
        }
    }
}