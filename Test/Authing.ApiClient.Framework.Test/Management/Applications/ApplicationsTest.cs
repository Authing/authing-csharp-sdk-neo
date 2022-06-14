using Authing.ApiClient.Domain.Model.Management.Applications;
using Authing.ApiClient.Domain.Model.Management.Resources;
using Authing.ApiClient.Domain.Model.Management.Roles;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Applications
{
    public class ApplicationsTest : BaseTest
    {
        [Fact]
        public async void Applications_List()
        {
            var client = managementClient;
            var result = await client.Applications.List();
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_Create()
        {
            var result = await managementClient.Applications.Create("测试3", "ceshi3", new string[] { "https://www.baidu.com" });
            Assert.Equal(result.Name, "测试3");
        }

        [Fact]
        public async void Applications_Delete()
        {
            var result = await managementClient.Applications.Delete("6215dd9277d6ef55dfab41f8");
            Assert.True(result);
        }

        [Fact]
        public async void Applications_FindById()
        {
            var result = await managementClient.Applications.FindById("6215dd9277d6ef55dfab41f8");

            //var ss= result.RegisterTabs.ToString();
            var ss = result.RegisterTabs;

            Assert.Equal(result.Name, "测试3");
        }

        [Fact]
        public async void Applications_ListResource()
        {
            var result = await managementClient.Applications.ListResource("6195ebcf5255f3d735ba9063");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_CreateResource()
        {
            var result = await managementClient.Applications.CreateResource("6195ebcf5255f3d735ba9063", new CreateResourceParam()
            {
                Code = "orderTest",
                Type = Types.ResourceType.DATA,
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "orderTest:read" } }
            });
            Assert.NotEmpty(result.Actions);
        }

        [Fact]
        public async void Applications_UpdateResource()
        {
            var result = await managementClient.Applications.UpdateResource("6195ebcf5255f3d735ba9063", "orderTest", new UpdateResourceParam()
            {
                Type = Types.ResourceType.DATA,
            });
            Assert.Equal(result.NameSpaceId, "6195ebcf5255f3d735ba9063");
        }

        [Fact]
        public async void Applications_DeleteResource()
        {
            var result = await managementClient.Applications.DeleteResource("6195ebcf5255f3d735ba9063", "orderTest");
            Assert.True(result);
        }

        [Fact]
        public async void Applications_GetAccessPolicies()
        {
            var result = await managementClient.Applications.GetAccessPolicies("6195ebcf5255f3d735ba9063", new AppAccessPolicyQueryFilter()
            {
                Page = 1,
                Limit = 10
            });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_EnableAccessPolicy()
        {
            var result = await managementClient.Applications.EnableAccessPolicy("6195ebcf5255f3d735ba9063", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DisableAccessPolicy()
        {
            var result = await managementClient.Applications.DisableAccessPolicy("6195ebcf5255f3d735ba9063", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DeleteAccessPolicy()
        {
            var result = await managementClient.Applications.DeleteAccessPolicy("6195ebcf5255f3d735ba9063", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_AllowAccess()
        {
            var result = await managementClient.Applications.AllowAccess("6195ebcf5255f3d735ba9063", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DenyAccess()
        {
            var result = await managementClient.Applications.DenyAccess("6195ebcf5255f3d735ba9063", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_UpdateDefaultAccessPolicy()
        {
            var result = await managementClient.Applications.UpdateDefaultAccessPolicy("6195ebcf5255f3d735ba9063", new UpdateDefaultApplicationAccessPolicyParam()
            {
                DefaultStrategy = Types.DefaultStrategyEnum.ALLOW_ALL
            });

            Assert.NotNull(result);
        }

        [Fact]
        public async void Applications_CreateRole()
        {
            var result = await managementClient.Applications.CreateRole("6195ebcf5255f3d735ba9063", "orderList");
            Assert.Equal(result.Code, "orderList");
        }

        [Fact]
        public async void Applications_DeleteRole()
        {
            var result = await managementClient.Applications.DeleteRole("6195ebcf5255f3d735ba9063", "orderList");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DeleteRoles()
        {
            var result = await managementClient.Applications.DeleteRoles("6195ebcf5255f3d735ba9063", new List<string>() { "orderList" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_UpdateRole()
        {
            var result = await managementClient.Applications.UpdateRole("6195ebcf5255f3d735ba9063", new UpdateRoleOptions()
            {
                Code = "orderList",
                NewCode = "orderList2",
                NameSpace = "6195ebcf5255f3d735ba9063"
            });
            Assert.Equal(result.Code, "orderList2");
        }

        [Fact]
        public async void Applications_FindRole()
        {
            var result = await managementClient.Applications.FindRole("6195ebcf5255f3d735ba9063", "orderList2");
            Assert.Equal(result.Code, "orderList2");
        }

        [Fact]
        public async void Applications_GetRoles()
        {
            var result = await managementClient.Applications.GetRoles("6195ebcf5255f3d735ba9063");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_GetUsersByRoleCode()
        {
            var result = await managementClient.Applications.GetUsersByRoleCode("6195ebcf5255f3d735ba9063", "userList");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_AddUsersToRole()
        {
            var result = await managementClient.Applications.AddUsersToRole("6195ebcf5255f3d735ba9063", "userList", new List<string>() { "61c05ea86623dbe950bd5831" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_RemoveUsersFromRole()
        {
            var result = await managementClient.Applications.RemoveUsersFromRole("6195ebcf5255f3d735ba9063", "userList", new List<string>() { "61c05ea86623dbe950bd5831" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_ListAuthorizedResourcesByRole()
        {
            var result = await managementClient.Applications.ListAuthorizedResourcesByRole("6195ebcf5255f3d735ba9063", "userList");
            Assert.Equal(result.Code, "userList");
        }

        [Fact]
        public async void Applications_createAgreement()
        {
            var result = await managementClient.Applications.createAgreement("6195ebcf5255f3d735ba9063", new AgreementInput()
            {
                Title = "userAgreement",
                Lang = Types.LangEnum.ZH_CN,
                Required = true
            });
            Assert.Equal(result.Title, "userAgreement");
        }

        [Fact]
        public async void Applications_deleteAgreement()
        {
            var result = await managementClient.Applications.deleteAgreement("6195ebcf5255f3d735ba9063", 0);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_modifyAgreement()
        {
            var result = await managementClient.Applications.modifyAgreement("6195ebcf5255f3d735ba9063", 0, new AgreementInput()
            {
                Title = "userAgreement2"
            });
            Assert.Equal(result.Title, "userAgreement2");
        }

        [Fact]
        public async void Applications_listAgreement()
        {
            var result = await managementClient.Applications.listAgreement("6195ebcf5255f3d735ba9063");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_sortAgreement()
        {
            var result = await managementClient.Applications.sortAgreement("6195ebcf5255f3d735ba9063", new List<int>() { 0 });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_ActiveUsers()
        {
            var result = await managementClient.Applications.ActiveUsers("6195ebcf5255f3d735ba9063");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_RefreshApplicationSecret()
        {
            var result = await managementClient.Applications.RefreshApplicationSecret("6195ebcf5255f3d735ba9063");
            Assert.NotNull(result);
        }

        [Fact]
        public async void Applications_ChangeApplicationType()
        {
            var result = await managementClient.Applications.ChangeApplicationType("6195ebcf5255f3d735ba9063", Types.ApplicationType.TENANT);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Applications_ApplicationTenants()
        {
            var result = await managementClient.Applications.ApplicationTenants("6195ebcf5255f3d735ba9063");
            Assert.NotNull(result);
        }
    }
}