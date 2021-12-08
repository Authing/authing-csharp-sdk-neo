namespace Authing.ApiClient.Domain.Model.Management.Statistics
{
    public class Geoip
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string CountryCode2 { get; set; }
        public string ContinentCode { get; set; }
        public string Ip { get; set; }
        public float Longitude { get; set; }
        public string CountryCode3 { get; set; }
        public float Latitude { get; set; }
        public Location Location { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string Timezone { get; set; }
    }
}