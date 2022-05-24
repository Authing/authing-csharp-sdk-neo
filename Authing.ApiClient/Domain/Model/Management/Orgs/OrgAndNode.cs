using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class OrgAndNode
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        [JsonProperty("rootNodeId")]
        public string RootNodeId { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameI18n")]
        public NameI18n NameI18n { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionI18n")]
        public string DescriptionI18n { get; set; }

        [JsonProperty("order")]
        public long? Order { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("leaderUserId")]
        public string LeaderUserId { get; set; }
    }
}