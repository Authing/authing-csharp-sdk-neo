using System;
using Newtonsoft.Json;
using Authing.ApiClient.Types;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class Resources
    {
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("actions")]
        public ResourceAction[] Actions { get; set; }

        [JsonProperty("type")]
        public ResourceType Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("namespaceId")]
        public string NameSpaceId { get; set; }

        [JsonProperty("apiIdentifier")]
        public string ApiIdentifier { get; set; }
    }

    public class ResourceAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class PaginatedResources
    {
        #region members
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<Resources> List { get; set; }
        #endregion
    }
}
