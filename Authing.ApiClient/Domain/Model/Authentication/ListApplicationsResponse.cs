using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ListApplicationsResponse
    {
        public int TotalCount { get; set; }
        public Application[] List { get; set; }

    }
}
