using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class ChangePhoneStrategy
    {
        #region members
        [JsonProperty("verifyOldPhone")]
        public bool? VerifyOldPhone { get; set; }
        #endregion
    }
}
