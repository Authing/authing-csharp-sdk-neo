using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class LoginPasswordFailCheckConfig
    {
        #region members
        [JsonProperty("timeInterval")]
        public int? TimeInterval { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
        #endregion
    }

}
