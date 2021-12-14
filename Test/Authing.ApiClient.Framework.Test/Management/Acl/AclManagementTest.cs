using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Acl
{
    public class AclManagementTest : BaseTest
    {
        [Fact]
        public async Task Acl_CreateNamespace()
        {
            var result = await managementClient.acl.CreateNamespace("testNameSpace", "testNameSpace", "testNameSpace");
            Assert.Equal(result.Code, "testNameSpace");
        }

        [Fact]
        public async Task Acl_ListNamespaces()
        {
            var result = await managementClient.acl.ListNamespaces();
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_UpdateNamespace()
        {
            //TODO:需要httpPut方法
            //64507
            var data = await managementClient.acl.CreateNamespace("testNameSpace2", "testNameSpace2", "testNameSpace2");
            var result = await managementClient.acl.UpdateNamespace(data.Id.ToString(), new UpdateNamespaceParam() { Code = "testNameSpace2", Name = "testNameSpace2", Description = "测试描述" });
            Assert.Equal(result.Description, "测试描述");
        }

        [Fact]
        public async Task Acl_DeleteNamespace()
        {
            var list = await managementClient.acl.ListNamespaces();
            var result = await managementClient.acl.DeleteNamespace(list.List.FirstOrDefault(i => i.Code == $"testNameSpace").Id);
            Assert.Equal(result, 200);
        }

        [Fact]
        public async Task Acl_ListResources()
        {
            var list = await managementClient.acl.ListResources(new ResourceQueryFilter() { NameSpaceCode = "test", Type = ResourceType.API });
            Assert.NotEmpty(list.List);
        }

        [Fact]
        public async Task Acl_CreateResource()
        {
            //TODO:Json转换报错，Actions属性从后端返回了String
            //var test1 = new ResourceParam()
            //{
            //    Code = new Random().Next().ToString(),
            //    NameSpace = "12345",
            //    Type = ResourceType.DATA,
            //    //NameSpace = "6172807001258f603126a78a",
            //    Actions = new List<ResourceAction>() { new ResourceAction() { Name = "123", Description = "123" } },
            //};
            //var s = test1.ConvertJson();
            string code = "";
            var list = await managementClient.acl.ListNamespaces();
            var id = list.List.FirstOrDefault(i => i.Code == "test")?.Code;
            var result = await managementClient.acl.CreateResource(new
                ResourceParam()
            {
                Code = code = new Random().Next().ToString(),
                NameSpace = id,
                Type = ResourceType.DATA,
                //NameSpace = "6172807001258f603126a78a",
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "123", Description = "123" } },
            });
            Assert.Equal(result.Code, code);
        }

        [Fact]
        public async Task Acl_GetResourceById()
        {
            var result = await managementClient.acl.GetResourceById("61b2ec3414381d31d2873221");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_FindResourceByCode()
        {
            var result = await managementClient.acl.FindResourceByCode("Book", "test");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_UpdateResource()
        {
            //var r = await managementClient.acl.FindResourceByCode("Book", "test");
            var result = await managementClient.acl.UpdateResource("Book", new ResourceParam() { Code = "Book", NameSpace = "test", Description = "HelloWord", Type = ResourceType.API });
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_DeleteResource()
        {
            string code = "";
            var list = await managementClient.acl.ListNamespaces();
            var id = list.List.FirstOrDefault(i => i.Code == "test")?.Code;
            var result = await managementClient.acl.CreateResource(new
                ResourceParam()
            {
                Code = code = new Random().Next().ToString(),
                NameSpace = id,
                Type = ResourceType.DATA,
                //NameSpace = "6172807001258f603126a78a",
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "123", Description = "123" } },
            });
            Assert.Equal(result.Code, code);

            Assert.True(await managementClient.acl.DeleteResource(code, id.ToString()));
        }

        [Fact]
        public async Task Acl_Allow()
        {
            var result = await managementClient.acl.Allow(TestUserId, "test", "Cat:*", "Cat:Read");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Acl_RevokeResource()
        {
            //TODO:opts.map is not a function
            var result = await managementClient.acl.RevokeResource(
                new RevokeResourceParams()
                {
                    NameSpace = "test",
                    Opts = new List<RevokeResourceOpt>()
                        {new RevokeResourceOpt(){TargetIdentifier = TestUserId,TargetType = PolicyAssignmentTargetType.USER}},
                    Resource = "Cat:test"
                });
        }

        [Fact]
        public async Task Acl_IsAllowed()
        {
            var result = await managementClient.acl.IsAllowed(TestUserId, "Cat:*", "Cat:Read", "test");
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_ListAuthorizedResources()
        {
            var result = await managementClient.acl.ListAuthorizedResources(
                PolicyAssignmentTargetType.USER, TestUserId,
                "test",
                new ListAuthorizedResourcesOptions() { ResourceType = ResourceType.DATA });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_GetAuthorizedTargets()
        {
            var result = await managementClient.acl.GetAuthorizedTargets(new GetAuthorizedTargetsOptions()
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
            var result = await managementClient.acl.AuthorizeResource("test", "Cat:read"
                , new List<AuthorizeResourceOpt>()
                {
                    new AuthorizeResourceOpt(PolicyAssignmentTargetType.USER,"61a5c55fc89ff91083293e45")
                });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async Task Acl_ProgrammaticAccessAccountList()
        {
            var result = await managementClient.acl.ProgrammaticAccessAccountList(new ProgrammaticAccessAccountListProps() { AppId = AppId });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_CreateProgrammaticAccessAccount()
        {
            //TODO:{"code":500,"message":"null value in column \"token_lifetime\" violates not-null constraint"}
            var result = await managementClient.acl.CreateProgrammaticAccessAccount(AppId,
                new CreateProgrammaticAccessAccountParam() { Remarks = "测试创建编程账户", AppId = AppId, Token_lifetime = 600 });
        }

        [Fact]
        public async Task Acl_DeleteProgrammaticAccessAccount()
        {
            var result = await managementClient.acl.DeleteProgrammaticAccessAccount("123");
            Assert.True(result);
        }

        [Fact]
        public async Task Acl_EnableProgrammaticAccessAccount()
        {
            var result = await managementClient.acl.EnableProgrammaticAccessAccount("123");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_DisableProgrammaticAccessAccount()
        {
            var result = await managementClient.acl.DisableProgrammaticAccessAccount("123");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Acl_GetApplicationAccessPolicies()
        {
            var result =
                await managementClient.acl.GetApplicationAccessPolicies(new AppAccessPolicyQueryFilter()
                { AppId = AppId });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async Task Acl_EnableApplicationAccessPolicy()
        {
            var result = await managementClient.acl.EnableApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
        }

        [Fact]
        public async Task Acl_DisableApplicationAccessPolicy()
        {
            var result = await managementClient.acl.DisableApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
        }

        [Fact]
        public async Task Acl_DeleteApplicationAccessPolicy()
        {
            var result = await managementClient.acl.DeleteApplicationAccessPolicy(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
        }

        [Fact]
        public async Task Acl_AllowApplicationAccessPolicy()
        {
            var result = await managementClient.acl.AllowAccessApplication(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
        }

        [Fact]
        public async Task Acl_DenyApplicationAccessPolicy()
        {
            var result = await managementClient.acl.DenyAccessApplication(new AppAccessPolicy()
            {
                AppId = AppId,
                InheritByChildren = null,
                NameSpace = "default",
                TargetType = PolicyAssignmentTargetType.USER,
                TartgetIdentifiers = new List<string>() { "61a5c55fc89ff91083293e45" }
            });
        }

        [Fact]
        public async Task Acl_UpdateDefaultApplicationAccessPolicy()
        {
            var result =
                await managementClient.acl.UpdateDefaultApplicationAccessPolicy(new DefaultAppAccessPolicy()
                {
                    AppId = AppId,
                    DefaultStrategy = DefaultStrategy.ALLOW_ALL,
                });
            Assert.NotNull(result);
        }

    }
}
