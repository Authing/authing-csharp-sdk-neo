using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class PhoneOrEmailBindableParam
    {
        //     @Expose
        //var phone: String? = null,
        [JsonProperty("phone")]
        public string Phone { get; set; }
        // @Expose
        // var email: String? = null,
        [JsonProperty("email")]
        public string Email { get; set; }
        // var mfaToken: String
        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
        
    }
}
