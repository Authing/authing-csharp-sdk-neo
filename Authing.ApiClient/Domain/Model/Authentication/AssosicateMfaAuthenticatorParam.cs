using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class AssosicateMfaAuthenticatorParam
    {
        [JsonProperty("authenticatorType")]
        public string AuthenticatorType { get; set; }

        [JsonProperty("mfaToken")]
        public string MafToken { get; set; }

        [JsonProperty("TotpSource")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TotpSource TotpSource { get; set; } = TotpSource.SELF;
    }
}
