using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class PasswordUpdatePolicyConfig
    {
        #region members
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("forcedCycle")]
        public int? ForcedCycle { get; set; }

        [JsonProperty("differenceCycle")]
        public int? DifferenceCycle { get; set; }
        #endregion
    }
}
