using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ListOrgsTest : BaseTest
    {
        [Fact]
        public async void ListOrgs_Test()
        {
            var client = managementClient;


            //for (int i = 0; i < 10; i++)
            //{
            //    await client.Orgs.Create($"组织机构{i}", "组织结构描述" + i);
            //}

            var result = await client.Orgs.List();

            Assert.True(result.TotalCount == 10);
        }
    }
}
