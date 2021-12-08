using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model
{
    public class AuthorizedResource
    {
        #region members
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public ResourceType? Type { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }
        #endregion
    }
}
