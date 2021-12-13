using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ListAuthorizedResourcesResponse
    {
        [JsonProperty("authorizedResources")]
        public PaginatedAuthorizedResources AuthorizedResources { get; set; }
    }
}