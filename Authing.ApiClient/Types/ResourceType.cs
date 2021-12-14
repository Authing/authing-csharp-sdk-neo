using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum ResourceType
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        [JsonProperty("DATA")]
        DATA,
        /// <summary>
        /// API 类型数据
        /// </summary>
        [JsonProperty("API")]
        API,
        /// <summary>
        /// 菜单类型数据
        /// </summary>
        [JsonProperty("MENU")]
        MENU,
        [JsonProperty("UI")]
        UI,
        /// <summary>
        ///  /// <summary>
        /// 按钮类型数据
        /// </summary>
        /// </summary>
        [JsonProperty("BUTTON")]
        BUTTON
    }
}
