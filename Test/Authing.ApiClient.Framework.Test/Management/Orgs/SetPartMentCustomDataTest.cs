using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.Library.Domain.Model.V3Model;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class SetPartMentCustomDataTest : BaseTest
    {
        /// <summary>
        /// 2022-8-10 测试通过
        /// </summary>
        [Fact]
        public async void SetPartMentCustomData_Test()
        {
            var client = managementClient;

            var res = await client.Orgs.SearchNodes("O1-C");

            var result = await client.Orgs.SetPartMentCustomData<SetCustomDataResponse>(res.First().Id, "isSecurity", "123");
            //result = await client.Orgs.SetPartMentCustomData<SetCustomDataResponse>(res.First().Id, "isDistribution", "123");
            Assert.NotNull(result.statusCode == 200);
        }

    }
}
