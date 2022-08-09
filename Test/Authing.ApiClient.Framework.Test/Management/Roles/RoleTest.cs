using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class RoleTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void CreateRole_Test()
        {
            var client = managementClient;

            var result = await client.Roles.Create("Admin",nameSpace:"default");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试不通过
        /// </summary>
        [Fact]
        public async void CreateRole_Test_WithDescription()
        {
            //TODO:接口报错，但是控制台创建成功
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void CreateRole_Test_WithDescription_ParenCode()
        {
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription", "Admin", "default");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void CreateRole_Test_WithDescription_Code()
        {
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription", null, "default");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestDelete";

            await client.Roles.Create(roleCode,nameSpace:"default");

            var error = new AuthingErrorBox();

            var result = await client.Roles.Delete(roleCode,nameSpace:"default",error);

            Assert.NotNull(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteRoleWithNameSpace_Test()
        {
            var client = managementClient;

            string roleCode = "TestNameSpace";

            string nameSpace = "default";

            var result = await client.Roles.Create(roleCode, null, null, nameSpace);

            Assert.NotNull(result);

            var message = await client.Roles.Delete(roleCode, nameSpace);

            Assert.True(message.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteManyRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestCode";
            string roleCode2 = "TestCode2";

            string nameSpace = "default";
            List<string> codeList = new List<string>();

            codeList.Add(roleCode);
            codeList.Add(roleCode2);

            await client.Roles.Create(roleCode,nameSpace:nameSpace);
            await client.Roles.Create(roleCode2,nameSpace:nameSpace);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Roles.DeleteMany(codeList,authingErrorBox);

            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteManyRoleWithNameSpace_Test()
        {
            var client = managementClient;

            string roleCode = "TestCode";
            string roleCode2 = "TestCode2";

            string nameSpace = "613189b38b6c66cac1d211bd";
            List<string> codeList = new List<string>();

            codeList.Add(roleCode);
            codeList.Add(roleCode2);

            await client.Roles.Create(roleCode, null, null, nameSpace);
            await client.Roles.Create(roleCode2, null, null, nameSpace);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

             

            var result = await client.Roles.DeleteMany(codeList, nameSpace,authingErrorBox);

            Assert.True(result.Code == 200);
        }     
        
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void UpdateRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestNameSpace";

            string nameSpace = "default";

            var result = await client.Roles.Create(roleCode, "RoleCodeDes", "test", nameSpace);

            Assert.NotNull(result);

            var newRole = await client.Roles.Update(new UpdateRoleOptions()
            {
                Code = result.Code,
                Description = "UpdateRoleCodeDes",
                NewCode = "TestNameSpaceUpdate",
                NameSpace = result.Namespace
            });

            result = await client.Roles.FindByCode("TestNameSpaceUpdate", nameSpace);

            Assert.True(result.Code == "TestNameSpaceUpdate");
        }
    }
}