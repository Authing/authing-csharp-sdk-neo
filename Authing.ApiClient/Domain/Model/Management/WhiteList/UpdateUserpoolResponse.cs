using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.WhiteList
{
    public class UpdateUserpoolResponse
    {

        [JsonProperty("updateUserpool")]
        public Model.UserPool Result { get; set; }
    }
}
