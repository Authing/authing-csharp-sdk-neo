using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Types;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class UdfTest : BaseTest
    {
        /// <summary>
        /// 2022-9-1 测试通过
        /// </summary>
        [Fact]
        public async void GetUdfValue_Test()
        {
            var client = managementClient;

            string roleCode = "userList";

            string nameSpace = "62a99822ff635db21c2ec21c";

            var role = await client.Roles.FindByCode(roleCode, nameSpace);

            KeyValueDictionary dic = new KeyValueDictionary();
            dic.Add("testrole", "testrole");

            var param = new SetUdfValueParam { RoleId = role.Id, UdvList = dic };

            var result = await client.Roles.SetUdfValue(param);

            var udfValues = await client.Roles.GetUdfValue("62aae37aa44bbb0427991d33");

            Assert.True(udfValues.Count() == 1);
        }
    }
}