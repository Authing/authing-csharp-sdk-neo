using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public enum LangEnum
    {
        [JsonProperty("zh-CN")]
        [EnumMember(Value = "zh-CN")]
        ZH_CN,

        [EnumMember(Value = "en-US")]
        [JsonProperty("en-US")]
        EN_US
    }
}