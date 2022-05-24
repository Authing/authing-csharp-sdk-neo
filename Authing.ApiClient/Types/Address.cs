using Newtonsoft.Json;

namespace Authing.ApiClient.Types
{
    public class Address
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }
}