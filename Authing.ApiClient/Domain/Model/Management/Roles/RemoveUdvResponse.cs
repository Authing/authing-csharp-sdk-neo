using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class RemoveUdvResponse
    {

        [JsonProperty("removeUdv")]
        public IEnumerable<UserDefinedData> Result { get; set; }
    }
}
