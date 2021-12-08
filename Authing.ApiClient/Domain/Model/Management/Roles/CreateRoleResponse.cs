using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Authing.ApiClient.Domain.Model.Management.Groups;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class CreateRoleResponse
    {

        [JsonProperty("createRole")]
        public Role Result { get; set; }
    }
}
