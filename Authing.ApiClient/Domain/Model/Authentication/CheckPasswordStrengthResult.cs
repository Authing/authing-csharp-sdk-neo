using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class CheckPasswordStrengthResult
    {
        #region members
        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
        #endregion
    }
}
