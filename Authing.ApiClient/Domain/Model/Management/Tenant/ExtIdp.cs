using System;
using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ExtIdpListOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TenantId { get; set; }
    }

    public class ExtIdpDetailOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TenantId { get; set; }
        public ExtIdpConnDetailOutput[] Connections { get; set; }
    }

    public class ExtIdpConnDetailOutput
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
    }

    public class CreateExtIdpOption
    {
        public string TenantId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ExtIdpType Type { get; set; }
        public ExtIdpConnDetailInput[] Connections { get; set; }
    }

    public class ExtIdpConnDetailInput
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ExtIdpConnType Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
    }

    public class UpdateExtIdpOption
    {
        public string Name { get; set; }
    }

    public class CreateExtIdpConnectionOption
    {
        public string ExtIdpId { get; set; }
        public ExtIdpConnType Type { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public IEnumerable<string> UserMatchFields { get; set; }
        public string Logo { get; set; }
    }

    public class UpdateExtIdpConnectionOption
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, object> Fields { get; set; }

        [JsonProperty("userMatchFields")]
        public IEnumerable<string> UserMatchFields { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }

    public class ChangeExtIdpConnectionStateOption
    {
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public bool Enabled { get; set; }
    }
}