using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    /// <summary>
    /// 如果Json返回的Json中Actions字段是字符串，必须使用此类接收
    /// </summary>
    public class Resources:ResourcesBase
    {
        [JsonProperty("actions")] 
        private string ActionsTemp { get; set; } = "";

        [JsonIgnore]
        public new IEnumerable<Action> Actions => JsonConvert.DeserializeObject<IEnumerable<Action>>(ActionsTemp);
    }

    public class ResourcesBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserPoolId { get; set; }
        public string Code { get; set; }
        public IEnumerable<Action> Actions { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int NamespaceId { get; set; }
        public object ApiIdentifier { get; set; }
        public string Namespace { get; set; }
    }
}