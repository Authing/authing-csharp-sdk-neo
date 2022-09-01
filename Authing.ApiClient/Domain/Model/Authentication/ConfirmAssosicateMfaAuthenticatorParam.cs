using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ConfirmAssosicateMfaAuthenticatorParam
    {
        //     @Expose
        //@SerializedName("authenticator_type")
        // var authenticatorType: String,
        [JsonProperty("authenticator_type")]
        public string AuthenticatorType { get; set; } = "totp";
        // @Expose
        // var totp: String? = null,
        [JsonProperty("totp")]
        public string Totp { get; set; }
        // @Expose
        // var source: TotpSource? = TotpSource.SELF,
        [JsonProperty("source")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TotpSource TotpSource { get; set; } = TotpSource.SELF;
        // var mfaToken: String? = null

        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
    }
}
