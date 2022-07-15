using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class RoleTest : BaseTest
    {
        [Fact]
        public async void CreateRole_Test()
        {
            var client = managementClient;

            var result = await client.Roles.Create("Admin");

            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateRole_Test_WithDescription()
        {
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription");

            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateRole_Test_WithDescription_ParenCode()
        {
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription", "12312313", "613189b38b6c66cac1d211bd");

            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateRole_Test_WithDescription_Code()
        {
            var client = managementClient;

            var result = await client.Roles.Create("AdminTest", "AdminDescription", null, "613189b38b6c66cac1d211bd");

            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestDelete";

            await client.Roles.Create(roleCode);

            var error = new Library.Domain.Model.Exceptions.AuthingErrorBox { };

            var result = await client.Roles.Delete(roleCode,error);

            Assert.NotNull(result.Code == 200);
        }

        [Fact]
        public async void DeleteRoleWithNameSpace_Test()
        {
            var client = managementClient;

            string roleCode = "TestNameSpace";

            string nameSpace = "613189b38b6c66cac1d211bd";

            var result = await client.Roles.Create(roleCode, null, null, nameSpace);

            Assert.NotNull(result);

            var message = await client.Roles.Delete(roleCode, nameSpace);

            Assert.True(message.Code == 200);
        }

        [Fact]
        public async void DeleteManyRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestCode";
            string roleCode2 = "TestCode2";

            string nameSpace = "613189b38b6c66cac1d211bd";
            List<string> codeList = new List<string>();

            codeList.Add(roleCode);
            codeList.Add(roleCode2);

            await client.Roles.Create(roleCode);
            await client.Roles.Create(roleCode2);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.Roles.DeleteMany(codeList,authingErrorBox);

            Assert.True(result.Code == 200);
        }

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

        [Fact]
        public async void UpdateRole_Test()
        {
            var client = managementClient;

            string roleCode = "TestNameSpace";

            string nameSpace = "613189b38b6c66cac1d211bd";

            var result = await client.Roles.Create(roleCode, "RoleCodeDes", "9527", nameSpace);

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