using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;
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
            var result = await managementClient.acl.UpdateNamespace("testNameSpace", new UpdateNamespaceParam() { Description = "测试描述" });
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
        async Task Acl_updateResource()
        {
            //var r = await managementClient.acl.FindResourceByCode("Book", "test");
            var result = await managementClient.acl.UpdateResource("Book", new ResourceParam() { Code = "Book",NameSpace = "test", Description = "HelloWord",Type = ResourceType.API});
            Assert.NotNull(result);
        }

        [Fact]
        async Task Acl_deleteResource()
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
        async Task Acl_Allow()
        {
            var result = await managementClient.acl.Allow(TestUserId, "test", "Cat:touch", "Cat:read");
            Assert.Equal(result.Code,200);
        }

        [Fact]
        async Task Acl_RevokeResource()
        {
            //TODO:opts.map is not a function
            var result = await managementClient.acl.RevokeResource(
                new RevokeResourceParams() {
                    NameSpace = "test",
                    Opts = new List<RevokeResourceOpt>()
                        {new RevokeResourceOpt(){TargetIdentifier = TestUserId,TargetType = PolicyAssignmentTargetType.USER}},
                    Resource = "Cat:touch"
                });
        }

        [Fact]
        async Task Acl_IsAllowed()
        {
            var result = await managementClient.acl.IsAllowed(TestUserId, "Cat:read", "Cat:touch","test");
            Assert.True(result);
        }

        [Fact]
        async Task Acl_listAuthorizedResources()
        {
            var result = await managementClient.acl.listAuthorizedResources(
                PolicyAssignmentTargetType.USER, TestUserId,
                "test",
                new ListAuthorizedResourcesOptions() { ResourceType = ResourceType.DATA });
            Assert.NotEmpty(result.List);
        }

    }
}
