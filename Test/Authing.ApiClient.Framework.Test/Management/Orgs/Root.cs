using System.Collections.Generic;

namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class TestNode
    {
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string order { get; set; }
        public IEnumerable<TestNode> children { get; set; }

    }
}