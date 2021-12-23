using Authing.ApiClient.Domain.Model.Management.Acl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PolicyStatement
    {
        #region members
        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }

        [JsonProperty("effect")]
        public PolicyEffect? Effect { get; set; }

        [JsonProperty("condition")]
        public IEnumerable<PolicyStatementCondition> Condition { get; set; }
        #endregion
    }
}
