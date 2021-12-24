using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class IMfaAuthenticator
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("createdAt")]
        public string CreateAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("authenticatorType")]
        public string AuthenticatorType { get; set; }

        [JsonProperty("recoveryCode")]
        public string RecoveryCode { get; set; }
    }
}
