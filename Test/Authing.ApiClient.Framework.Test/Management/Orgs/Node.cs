namespace Authing.ApiClient.Framework.Test.Management.Orgs
{
    public class Node
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Node[] children { get; set; }
    }
}