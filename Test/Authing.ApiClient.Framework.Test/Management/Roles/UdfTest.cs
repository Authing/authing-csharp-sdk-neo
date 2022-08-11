using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Types;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class UdfTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试不通过
        /// </summary>
        [Fact]
        public async void GetUdfValue_Test()
        {
            var client = managementClient;

            string roleCode = "test";

            string nameSpace = "default";

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