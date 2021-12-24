using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
   public class GetMfaAuthenticatorsResponse
    {
        [JsonProperty("Result")]
        public List<IMfaAuthenticator> Result { get; set; }
    }
}
