using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class BindEmailResponse
    {

        [JsonProperty("bindEmail")]
        public User Result { get; set; }
    }
}
