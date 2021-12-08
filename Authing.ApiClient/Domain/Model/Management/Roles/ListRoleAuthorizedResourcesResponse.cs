using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class ListRoleAuthorizedResourcesResponse
    {

        [JsonProperty("role")]
        public Role Result { get; set; }
    }

}
