using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class Geoip
    {
        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }

        [JsonProperty("country_code2")]
        public string CountryCode2 { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("country_code3")]
        public string CountryCode3 { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

    }
}


