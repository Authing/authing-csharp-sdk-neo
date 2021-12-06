using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UdfResponse
    {

        [JsonProperty("udf")]
        public IEnumerable<UserDefinedField> Result { get; set; }
    }
}
