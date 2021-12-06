using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class CustomSMSProvider
    {
        #region members
        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("config")]
        public string Config { get; set; }
        #endregion
    }
}
