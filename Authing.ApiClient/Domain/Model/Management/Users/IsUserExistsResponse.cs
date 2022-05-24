using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class IsUserExistsResponse
    {

        [JsonProperty("isUserExists")]
        public bool? Result { get; set; }
    }
}
