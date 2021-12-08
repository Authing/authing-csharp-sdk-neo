using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class ChangeEmailStrategy
    {
        #region members
        [JsonProperty("verifyOldEmail")]
        public bool? VerifyOldEmail { get; set; }
        #endregion
    }
}
