using System;
using Newtonsoft.Json;
using Authing.ApiClient.Types;
using System.Collections.Generic;
namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class ListResourceOption
    {
        public int? Page { get; set; } = 1;

        public int? Limit { get; set; } = 10;

        public ResourceType? Type { get; set; }
    }

    public class ListResourceParam
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("type")]
        public ResourceType? Type { get; set; }
    }

    public class ListResourceRes
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public PaginatedResources Data { get; set; }
    }
}
