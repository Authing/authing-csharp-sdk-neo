using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class AgreementInput
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("lang")]
        public LangEnum Lang { get; set; }

        [JsonProperty("availableAt")]
        public AvailableAt AvailableAt { get; set; }
    }
}