using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class MFALoginResponse
    {
        [JsonProperty("mfaToken")]
        public string MfaToken { get; set; }
        [JsonProperty("nickname")]
        public object NickName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public object Phone { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("faceMfaEnabled")]
        public bool FaceMafEnabled { get; set; }
        [JsonProperty("totpMfaEnabled")]
        public bool TotpMfaEnabled { get; set; }
        [JsonProperty("applicationMfa")]
        public Applicationmfa[] ApplicationMfa { get; set; }
    }

    public class Applicationmfa
    {
        [JsonProperty("mfaPolicy")]
        public string MfaPolicy { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("sort")]
        public int Sort { get; set; }
    }

}
