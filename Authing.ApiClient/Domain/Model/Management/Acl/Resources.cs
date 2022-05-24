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
}