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
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_List()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            var result = await client.Applications.List(authingErrorBox: authingErrorBox);
            Assert.NotEmpty(result.List); 
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_Create()
        {

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();


            var result = await managementClient.Applications.Create("testbytmgg", "testbytmgg", new string[] { "https://www.baidu.com" },
                authingErrorBox: authingErrorBox);
            Assert.Equal(result.Name, "testbytmgg");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_Delete()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var lists = await managementClient.Applications.List(authingErrorBox: authingErrorBox);

            var res = lists.List.First(i => i.Name == "testbytmgg");

            var result = await managementClient.Applications.Delete(res.Id, authingErrorBox: authingErrorBox);
            Assert.True(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_FindById()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.FindById("62a99822ff635db21c2ec21c", authingErrorBox);

            Assert.Equal(result.Name, "testresource");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_FindByIdV2()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();


            var result = await managementClient.Applications.FindByIdV2("62a99822ff635db21c2ec21c", authingErrorBox);

            //var ss= result.RegisterTabs.ToString();
            var ss = result.LoginTabs;

            Assert.NotNull(ss);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_ListResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.ListResource("61c2d04b36324259776af784",new ListResourceOption(){Type = ResourceType.DATA}, authingErrorBox: authingErrorBox);
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
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

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
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

            Assert.Equal(result.Actions.First().Name, "orderTest:write");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_DeleteResource()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteResource("62a99822ff635db21c2ec21c", "orderTest", authingErrorBox);
            Assert.True(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
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

        /// <summary>
        /// 2022-8-10 测试通过
        /// TargetIdentifiers 传递的是 ID 非 CODE
        /// </summary>
        [Fact]
        public async void Applications_EnableAccessPolicy()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.EnableAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "62aae37aa44bbb0427991d33" },
                InheritByChildren = true
            });
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// TargetIdentifiers 传递的是 ID 非 CODE
        /// </summary>
        [Fact]
        public async void Applications_DisableAccessPolicy()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DisableAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "62aae37aa44bbb0427991d33" },
                InheritByChildren = true
            },authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// TargetIdentifiers 传递的是 ID 非 CODE
        /// </summary>
        [Fact]
        public async void Applications_DeleteAccessPolicy()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteAccessPolicy("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "62aae37aa44bbb0427991d33" },
                InheritByChildren = true
            }, authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试不通过
        /// TargetIdentifiers 传递的是 ID 非 CODE
        /// </summary>
        [Fact]
        public async void Applications_AllowAccess()
        {
            //TODO:无法在控制台应用访问控制中看到数据
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.AllowAccess("62a99822ff635db21c2ec21c", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.ROLE,
                TargetIdentifiers = new string[] { "62aae37aa44bbb0427991d33" },
                InheritByChildren = true
            }, authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试不通过
        /// TargetIdentifiers 传递的是 ID 非 CODE
        /// </summary>
        [Fact]
        public async void Applications_DenyAccess()
        {
            //TODO:无法在控制台应用访问控制中看到数据
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DenyAccess("62a9902a80f55c22346eb296", new AppAccessPolicy()
            {
                TargetType = Types.PolicyAssignmentTargetType.USER,
                TargetIdentifiers = new string[] { "62bc37200d0fc2db637e92ef" },
                InheritByChildren = true
            },authingErrorBox);
            Assert.Equal(200,result.Code );
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_UpdateDefaultAccessPolicy()
        {
            var result = await managementClient.Applications.UpdateDefaultAccessPolicy("62a99822ff635db21c2ec21c", new UpdateDefaultApplicationAccessPolicyParam()
            {
                DefaultStrategy = Types.DefaultStrategyEnum.ALLOW_ALL
            });

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_CreateRole()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.CreateRole("62a99822ff635db21c2ec21c", "orderList",
               authingErrorBox: authingErrorBox);
            Assert.Equal(result.Code, "orderList");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_DeleteRole()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await managementClient.Applications.DeleteRole("62a99822ff635db21c2ec21c", "orderList", authingErrorBox);
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_DeleteRoles()
        {
            var result = await managementClient.Applications.DeleteRoles("62a99822ff635db21c2ec21c", new List<string>() { "orderList" });
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_UpdateRole()
        {
            var result = await managementClient.Applications.UpdateRole("62a99822ff635db21c2ec21c", new UpdateRoleOptions()
            {
                Code = "orderList",
                NewCode = "orderList2",
                NameSpace = "62a99822ff635db21c2ec21c"
            });
            Assert.Equal(result.Code, "orderList2");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_FindRole()
        {
            var result = await managementClient.Applications.FindRole("62a99822ff635db21c2ec21c", "orderList2");
            Assert.Equal(result.Code, "orderList2");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_GetRoles()
        {
            var result = await managementClient.Applications.GetRoles("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_GetUsersByRoleCode()
        {
            var result = await managementClient.Applications.GetUsersByRoleCode("62a99822ff635db21c2ec21c", "userList");
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_AddUsersToRole()
        {
            var result = await managementClient.Applications.AddUsersToRole("62a99822ff635db21c2ec21c", "userList", new List<string>() { TestUserId });
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_RemoveUsersFromRole()
        {
            var result = await managementClient.Applications.RemoveUsersFromRole("62a99822ff635db21c2ec21c", "userList", new List<string>() { TestUserId });
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_ListAuthorizedResourcesByRole()
        {
            var result = await managementClient.Applications.ListAuthorizedResourcesByRole("62a99822ff635db21c2ec21c", "userList");
            Assert.Equal(result.AuthorizedResources.List.First().Code, "book:*");
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// qidong 添加在界面展示的参数后测试通过
        /// </summary>
        [Fact]
        public async void Applications_createAgreement()
        {
            var result = await managementClient.Applications.createAgreement("6215dd9277d6ef55dfab41f8", new AgreementInput()
            {
                Title = "userAgreement",
                Lang = Types.LangEnum.ZH_CN,
                Required = true
            });
            Assert.Equal("userAgreement",result.Title);
        }

        /// <summary>
        /// 2022-8-12 测试不通过
        /// qidong  传入的协议 id 要正确，才能删除
        /// </summary>
        [Fact]
        public async void Applications_deleteAgreement()
        {
            var result = await managementClient.Applications.deleteAgreement("6215dd9277d6ef55dfab41f8", 1017);
            Assert.Equal(200,result.Code );
        }

        /// <summary>
        /// 2022-8-10 测试不通过
        /// </summary>
        [Fact]
        public async void Applications_modifyAgreement()
        {
            var agreement = await managementClient.Applications.createAgreement("6215dd9277d6ef55dfab41f8", new AgreementInput()
            {
                Title = "userAgreement",
                Lang = Types.LangEnum.ZH_CN,
                Required = true
            });

            agreement.Title = "userAgreementUpdate";

            var result = await managementClient.Applications.modifyAgreement("62a99822ff635db21c2ec21c", agreement.Id, new AgreementInput 
            {
                AvailableAt=agreement.AvailableAt,
                Lang=LangEnum.ZH_CN,
                Required=agreement.Required,
                Title=agreement.Title
            });
            Assert.Equal("userAgreementUpdate",result.Title);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_listAgreement()
        {
            var result = await managementClient.Applications.listAgreement("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_sortAgreement()
        {
            var result = await managementClient.Applications.sortAgreement("62a99822ff635db21c2ec21c", new List<int>() { 0 });
            Assert.Equal(result.Code, 200);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_ActiveUsers()
        {
            var result = await managementClient.Applications.ActiveUsers("62a99822ff635db21c2ec21c");
            Assert.NotEmpty(result.List);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_RefreshApplicationSecret()
        {
            var result = await managementClient.Applications.RefreshApplicationSecret("62a99822ff635db21c2ec21c");
            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_ChangeApplicationType()
        {
            var result = await managementClient.Applications.ChangeApplicationType("62a99822ff635db21c2ec21c", Types.ApplicationType.BOTH);
            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void Applications_ApplicationTenants()
        {
            var result = await managementClient.Applications.ApplicationTenants("62a99822ff635db21c2ec21c");
            Assert.NotNull(result);
        }
    }
}