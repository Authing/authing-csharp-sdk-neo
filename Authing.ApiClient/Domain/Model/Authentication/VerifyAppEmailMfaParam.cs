using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class VerifyAppEmailMfaParam
    {
        //      @Expose
        //var email: String,
        [JsonProperty("email")]
        public string Email { get; set; }
        //  @Expose
        //  var code: String,
        [JsonProperty("code")]
        public string Code { get; set; }
        //  var mfaToken: String
        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
    }
}
