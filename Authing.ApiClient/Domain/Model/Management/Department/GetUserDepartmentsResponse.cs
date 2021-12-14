using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Department
{
    public class GetUserDepartmentsResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}
