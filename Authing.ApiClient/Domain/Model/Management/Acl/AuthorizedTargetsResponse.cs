using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AuthorizedTargetsResponse
    {

        [JsonProperty("authorizedTargets")]
        public PaginatedAuthorizedTargets Result { get; set; }
    }
}