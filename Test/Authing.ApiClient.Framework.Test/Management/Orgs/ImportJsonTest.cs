using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class ImportJsonTest : BaseTest
    {
        [Fact]
        public async void ImprotJson_Test()
        {
            var client = managementClient;

            var ss = await client.Orgs.List();

            Root root = new Root();
            root.name = "根节点";
            root.code = "9527";
            //root.children = new Node[1];

            //root.children[0] = new Node();

            //root.children[0].name = "运营";
            //root.children[0].children = new Node[1];

            //root.children[0].children[0] = new Node();

            //root.children[0].children[0].name = "后端";

            await client.Userpool.RemoveEnv("1");

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(root);

            JsontObj ob = new JsontObj();
            ob.filetype = "json";
            ob.file = root;

            string js = Newtonsoft.Json.JsonConvert.SerializeObject(ob);

            var result = await client.Orgs.ImportByJson(js);

            Assert.NotNull(result);
        }
    }
}