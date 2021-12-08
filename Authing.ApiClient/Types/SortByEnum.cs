using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum SortByEnum
    {
        /// <summary>
        /// 按照创建时间降序（后创建的在前面）
        /// </summary>
        [JsonProperty("CREATEDAT_DESC")]
        CREATEDAT_DESC,
        /// <summary>
        /// 按照创建时间升序（先创建的在前面）
        /// </summary>
        [JsonProperty("CREATEDAT_ASC")]
        CREATEDAT_ASC,
        /// <summary>
        /// 按照更新时间降序（最近更新的在前面）
        /// </summary>
        [JsonProperty("UPDATEDAT_DESC")]
        UPDATEDAT_DESC,
        /// <summary>
        /// 按照更新时间升序（最近更新的在后面）
        /// </summary>
        [JsonProperty("UPDATEDAT_ASC")]
        UPDATEDAT_ASC
    }
}
