using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class GetUserGroupsResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}
