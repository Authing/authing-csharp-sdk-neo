using Authing.ApiClient.Domain.Model.Management.Udf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class SetUdvResponse
    {

        [JsonProperty("setUdv")]
        public IEnumerable<UserDefinedData> Result { get; set; }
    }
}
