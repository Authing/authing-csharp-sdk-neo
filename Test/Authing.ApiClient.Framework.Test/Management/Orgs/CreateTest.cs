using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class CreateTest : BaseTest
    {
        [Fact]
        public async void CreateOrgs()
        {
            var client = managementClient;

            var result=await  client.Orgs.Create("组织结构1","组织结构1的描述","9527");

            Assert.NotNull(result);
        }

       
    }
}
