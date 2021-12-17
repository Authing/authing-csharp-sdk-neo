using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model
{
    public class RefreshToken
    {
        #region members
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("iat")]
        public int? Iat { get; set; }

        [JsonProperty("exp")]
        public int? Exp { get; set; }
        #endregion
    }
}
