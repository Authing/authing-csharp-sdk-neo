using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Types;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class UdfTest : BaseTest
    {
        [Fact]
        public async void GetUdfValue_Test()
        {
            var client = managementClient;

            string roleCode = "admin";

            string nameSpace = "613189b38b6c66cac1d211bd";

            var role = await client.Roles.FindByCode(roleCode, nameSpace);

            KeyValueDictionary dic = new KeyValueDictionary();
            dic.Add("adminKey", "adminValue");

            var param = new SetUdfValueParam { RoleId = role.Id, UdvList = dic };

            var result = await client.Roles.SetUdfValue(param);

            var udfValues = await client.Roles.GetUdfValue(roleCode);

            Assert.True(udfValues.Count() == 1);

            //TODO: 添加拓展字段失败
        }
    }
}