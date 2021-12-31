using System;
using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class Agreement
    {
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class AgreementInput
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("lang")]
        public LangEnum Lang { get; set; }
    }

    public class AgreementRes
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Agreement Data { get; set; }
    }

    public class PaginationAgreement
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("list")]
        public IEnumerable<Agreement> List { get; set; }
    }
}
