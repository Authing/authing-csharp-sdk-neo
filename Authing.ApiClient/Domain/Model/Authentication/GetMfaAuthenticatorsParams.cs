using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class GetMfaAuthenticatorsParam
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "totp";

        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }

        [JsonProperty("TotpSource")]
        public TotpSourceEnum TotpSource { get; set; }

        public GetMfaAuthenticatorsParam()
        {
            TotpSource =TotpSourceEnum.SELF;
        }
    }
}
