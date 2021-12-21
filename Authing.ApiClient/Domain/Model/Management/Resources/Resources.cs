using System;
using Newtonsoft.Json;
using Authing.ApiClient.Types;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class Resources
    {
        public string UserPoolId { get; set; }
        public string Code { get; set; }
        public ResourceAction[] Actions { get; set; }
        public ResourceType Type { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public string NameSpaceId { get; set; }
        public string ApiIdentifier { get; set; }
    }

    public class ResourceAction
    {
        public string Name { get; set; }
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
