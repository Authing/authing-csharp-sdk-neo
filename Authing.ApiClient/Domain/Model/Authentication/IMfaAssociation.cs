using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class IMfaAssociation
    {
        // var authenticator_type: String,
        [JsonProperty("authenticator_type")]
        public string AuthenticatorType { get; set; }


        //var secret: String,
        [JsonProperty("secret")]
        public string Secret { get; }
        //var qrcode_uri: String,
        [JsonProperty("qrcode_uri")]
        public string QRCodeUri { get; set; }
        //var qrcode_data_url: String,
        [JsonProperty("qrcode_data_url")]
        public string QRcodeDataUrl { get; set; }
        //var recovery_code: String

        [JsonProperty("recovery_code")]
        public string RecoveryCode { get; set; }
    }
}
