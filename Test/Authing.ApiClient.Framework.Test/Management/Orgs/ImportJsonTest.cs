using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

          string js=  Newtonsoft.Json.JsonConvert.SerializeObject(ob);

            var result = await client.Orgs.ImportByJson(js);

            Assert.NotNull(result);
        }
    }

    public class JsontObj
    { 
        public string filetype { get; set; }
        public Root file { get; set; }
    }


    public class Root
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Node
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Node[] children { get; set; }
    }



}
