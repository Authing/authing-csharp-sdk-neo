using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class VerifyTotpMfaParam
    {
        [JsonProperty("totp")]
        public string Totp { get; set; }

        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
    }
}
