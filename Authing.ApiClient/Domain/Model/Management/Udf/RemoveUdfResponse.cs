using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class RemoveUdfResponse
    {

        [JsonProperty("removeUdf")]
        public CommonMessage Result { get; set; }
    }
}
