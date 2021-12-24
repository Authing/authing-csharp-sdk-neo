using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class DisableAssignmentResponse
    {
        [JsonProperty("disbalePolicyAssignment")]
        public CommonMessage Result { get; set; }
    }
}
