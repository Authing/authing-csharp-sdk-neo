using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class UserDefinedDataMap
    {
        #region members
        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("data")]
        public IEnumerable<UserDefinedData> Data { get; set; }
        #endregion
    }
}
