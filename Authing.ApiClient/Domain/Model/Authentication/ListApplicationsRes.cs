using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ListApplicationsRes
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public ApplicationList Data { get; set; }

    }
}
