using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Types;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Udf
{
    public class ManagementClientUdfListUdvTest : BaseTest
    {
        [Fact]
        public async void ListUserDefinedField_User()
        {
            var client = managementClient;

            KeyValueDictionary dic = new KeyValueDictionary();

            for (int i = 0; i < 10; i++)
            {
                dic.Add("user" + i.ToString(), i.ToString());
            }

            var addResult = await client.Udf.SetUdvBatch(UdfTargetType.USER, "userUdv", dic);

            IEnumerable<ResUdv> result = await client.Udf.ListUdv(UdfTargetType.USER, "userUdv");

            Assert.NotNull(result.Count() > 0);
        }
    }
}