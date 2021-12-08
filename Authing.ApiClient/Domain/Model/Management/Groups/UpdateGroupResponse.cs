using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class UpdateGroupResponse
    {

        [JsonProperty("updateGroup")]
        public Group Result { get; set; }
    }
}
