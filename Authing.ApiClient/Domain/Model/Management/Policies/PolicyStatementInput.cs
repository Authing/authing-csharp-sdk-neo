using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PolicyStatementInput
    {
        #region members
        [JsonProperty("resource")]
        [JsonRequired]
        public string Resource { get; set; }

        [JsonProperty("actions")]
        [JsonRequired]
        public IEnumerable<string> Actions { get; set; }

        [JsonProperty("effect")]
        public PolicyEffect? Effect { get; set; }

        [JsonProperty("condition")]
        public IEnumerable<PolicyStatementConditionInput> Condition { get; set; }
        #endregion


        /// <summary>
        /// <param name="resource">resource</param>
        /// <param name="actions">actions</param>
        /// </summary>

        public PolicyStatementInput(string resource, IEnumerable<string> actions)
        {
            this.Resource = resource;
            this.Actions = actions;
        }

        #region methods
        public dynamic GetInputObject()
        {
            return ReflectionHelper.GetInputObjec(this);
        }
        #endregion
    }
}
