using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class VerifyTotpRecoveryCodeParam
    {
        //      @Expose
        //var recoveryCode: String,
        [JsonProperty("recoveryCode")]
        public string RecoveryCode { get; set; }
        //  var mfaToken: String
        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
    }
}
