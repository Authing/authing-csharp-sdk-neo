using Authing.ApiClient.Domain.Model.Management.Applications;
using Authing.ApiClient.Domain.Model.Management.Resources;
using Authing.ApiClient.Domain.Model.Management.Roles;
using System.Collections.Generic;
using System.Linq;
using Authing.ApiClient.Types;
using Xunit;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Framework.Test.Management.Applications
{
    public class ApplicationsTest : BaseTest
    {
        [Fact]
        public async void Applications_List()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var result = await client.Applications.List(authingErrorBox: authingErrorBox);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_Create()
        {

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.Create("测试3", "ceshi3", new string[] { "https://www.google.com" },
                authingErrorBox: authingErrorBox);
            Assert.Equal(result.Name, "测试3");
        }

        [Fact]
        public async void Applications_Delete()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.Delete("62c54e9063ecf47a61810298", authingErrorBox: authingErrorBox);
            Assert.True(result);
        }

        [Fact]
        public async void Applications_FindById()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.FindById("6215dd9277d6ef55dfab41sss", authingErrorBox);

            Assert.Equal(result.Name, "测试3");
        }

        [Fact]
        public async void Applications_FindByIdV2()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();


            var result = await managementClient.Applications.FindByIdV2("62a9902a80f55c22346eb296", authingErrorBox);

            //var ss= result.RegisterTabs.ToString();
            var ss = result.LoginTabs;

            Assert.NotNull(ss);
        }

        [Fact]
        public async void Applications_ListResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.ListResource("62a99822ff635db21c2ec21c", authingErrorBox: authingErrorBox);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_CreateResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.CreateResource("62a99822ff635db21c2ec21c", new CreateResourceParam()
            {
                Code = "orderTest",
                Type = Types.ResourceType.DATA,
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "orderTest:read" } }
            }, authingErrorBox);
            Assert.NotEmpty(result.Actions);
        }

        [Fact]
        public async void Applications_UpdateResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();
            //var res = await managementClient.Applications.ListResource("62a99822ff635db21c2ec21c");
            var result = await managementClient.Applications.UpdateResource("62a99822ff635db21c2ec21c", "orderTest", new UpdateResourceParam()
            {
                Type = Types.ResourceType.DATA,
                Actions = new List<ResourceAction>() { new ResourceAction() { Name = "orderTest:write" } }
            }, authingErrorBox);

            Assert.Equal(result.NameSpaceId, "6195ebcf5255f3d735ba9063");
        }

        [Fact]
        public async void Applications_DeleteResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteResource("62a99822ff635db21c2ec21c", "orderTest", authingErrorBox);
            Assert.True(result);
        }

        [Fact]
        public async void Applications_GetAccessPolicies()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.GetAccessPolicies("62a99822ff635db21c2ec21c", new AppAccessPolicyQueryFilter()
            {
                Page = 1,
                Limit = 10
            },authingErrorBox);
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_EnableAccessPolicy()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.EnableAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
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
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DisableAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            },authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DeleteAccessPolicy()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            }, authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_AllowAccess()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.AllowAccess("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            }, authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DenyAccess()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DenyAccess("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "userList" },
                InheritByChildren = true
            },authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_UpdateDefaultAccessPolicy()
        {
            var result = await managementClient.Applications.UpdateDefaultAccessPolicy("62a99822ff635db21c2ec21c", new UpdateDefaultApplicationAccessPolicyParam()
            {
                DefaultStrategy = Types.DefaultStrategyEnum.ALLOW_ALL
            });

            Assert.NotNull(result);
        }

        [Fact]
        public async void Applications_CreateRole()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.CreateRole("62a99822ff635db21c2ec21c", "orderList",
               authingErrorBox: authingErrorBox);
            Assert.Equal(result.Code, "orderList");
        }

        [Fact]
        public async void Applications_DeleteRole()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteRole("62a99822ff635db21c2ec21c", "orderList", authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_DeleteRoles()
        {
            var result = await managementClient.Applications.DeleteRoles("62a99822ff635db21c2ec21c", new List<string>() { "orderList" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_UpdateRole()
        {
            var result = await managementClient.Applications.UpdateRole("62a99822ff635db21c2ec21c", new UpdateRoleOptions()
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
            var result = await managementClient.Applications.FindRole("62a99822ff635db21c2ec21c", "orderList2");
            Assert.Equal(result.Code, "orderList2");
        }

        [Fact]
        public async void Applications_GetRoles()
        {
            var result = await managementClient.Applications.GetRoles("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_GetUsersByRoleCode()
        {
            var result = await managementClient.Applications.GetUsersByRoleCode("62a99822ff635db21c2ec21c", "userList");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_AddUsersToRole()
        {
            var result = await managementClient.Applications.AddUsersToRole("62a99822ff635db21c2ec21c", "userList", new List<string>() { "62147e6b5e21c7b1c402e144" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_RemoveUsersFromRole()
        {
            var result = await managementClient.Applications.RemoveUsersFromRole("62a99822ff635db21c2ec21c", "userList", new List<string>() { "62147e6b5e21c7b1c402e144" });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_ListAuthorizedResourcesByRole()
        {
            var result = await managementClient.Applications.ListAuthorizedResourcesByRole("62a99822ff635db21c2ec21c", "userList");
            Assert.Equal(result.Code, "userList");
        }

        [Fact]
        public async void Applications_createAgreement()
        {
            /*
             {
               "code": 400,
               "statusCode": 400,
               "message": "参数格式错误",
               "data": {
               "detailedMessage": "An instance of AgreementDto has failed the validation:\n - property lang has failed the following constraints: isNotEmpty, isEnum \n"
               }
             }
             */
            var result = await managementClient.Applications.createAgreement("62a99822ff635db21c2ec21c", new AgreementInput()
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
            var result = await managementClient.Applications.deleteAgreement("62a99822ff635db21c2ec21c", 0);
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_modifyAgreement()
        {
            /*
             {
               "code": 400,
               "statusCode": 400,
               "message": "参数格式错误",
               "data": {
               "detailedMessage": "An instance of AgreementDto has failed the validation:\n - property lang has failed the following constraints: isNotEmpty, isEnum \n"
               }
             }
             */
            var result = await managementClient.Applications.modifyAgreement("62a99822ff635db21c2ec21c", 0, new AgreementInput()
            {
                Title = "userAgreement2",
            });
            Assert.Equal(result.Title, "userAgreement2");
        }

        [Fact]
        public async void Applications_listAgreement()
        {
            var result = await managementClient.Applications.listAgreement("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_sortAgreement()
        {
            var result = await managementClient.Applications.sortAgreement("62a99822ff635db21c2ec21c", new List<int>() { 0 });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Applications_ActiveUsers()
        {
            var result = await managementClient.Applications.ActiveUsers("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Applications_RefreshApplicationSecret()
        {
            var result = await managementClient.Applications.RefreshApplicationSecret("6172807001258f603126a78a");
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