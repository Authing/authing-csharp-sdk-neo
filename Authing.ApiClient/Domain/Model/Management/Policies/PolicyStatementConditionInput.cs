using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Policies
{
    public class PolicyStatementConditionInput
    {
        #region members
        [JsonProperty("param")]
        [JsonRequired]
        public string Param { get; set; }

        [JsonProperty("operator")]
        [JsonRequired]
        public string Operator { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public object Value { get; set; }
        #endregion


        /// <summary>
        /// <param name="param">param</param>
        /// <param name="operator">operator</param>
        /// <param name="value">value</param>
        /// </summary>

        public PolicyStatementConditionInput(string param, string _operator, object value)
        {
            this.Param = param;
            this.Operator = _operator;
            this.Value = value;
        }

        #region methods
        public dynamic GetInputObject()
        {
            return ReflectionHelper.GetInputObjec(this);
        }
        #endregion
    }
}
