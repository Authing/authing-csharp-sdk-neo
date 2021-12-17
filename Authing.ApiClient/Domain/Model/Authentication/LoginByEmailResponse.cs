using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class LoginByEmailResponse
    {

        [JsonProperty("loginByEmail")]
        public User Result { get; set; }
    }
}
