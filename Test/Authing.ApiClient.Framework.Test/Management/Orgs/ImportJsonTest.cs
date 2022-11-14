using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Extensions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ImportJsonTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试不通过
        /// </summary>
        [Fact]
        public  void ImprotJson_Test()
        {
            //TODO:{"uniqueId":"4725f6cc-2f08-4aa6-9f62-a4f6688ab6d2","code":500,"statusCode":499,"apiCode":500,"message":"不支持的导入类型"}
            var client = managementClient;

            TestNode root = new TestNode();
            root.name = "根节点";
            root.code = "9527";
            root.order = "10";
            root.description = "test";
            root.children = new List<TestNode>();

            var result =  client.Orgs.ImportByJson(root.ConvertJson()).Result;

            Assert.NotNull(result);
        }
    }
}