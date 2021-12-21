using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class Location
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}
