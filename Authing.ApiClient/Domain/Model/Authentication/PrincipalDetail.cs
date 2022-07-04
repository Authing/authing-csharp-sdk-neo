using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Model.Authentication
{

    public class PrincipalDetail
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("principalType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PrincipalType PrincipalType { get; set; }

        [JsonProperty("principalName")]
        public string PrincipalName { get; set; }

        [JsonProperty("principalCode")]
        public string PrincipalCode { get; set; }

        [JsonProperty("authenticationTime")]
        public DateTime AuthenticationTime { get; set; }
    }

}
