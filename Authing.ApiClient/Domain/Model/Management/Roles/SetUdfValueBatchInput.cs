using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class SetUdfValueBatchInput
    {
        #region members
        [JsonProperty("targetId")]
        [JsonRequired]
        public string TargetId { get; set; }

        [JsonProperty("key")]
        [JsonRequired]
        public string Key { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public string Value { get; set; }
        #endregion


        /// <summary>
        /// <param name="targetId">targetId</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// </summary>

        public SetUdfValueBatchInput(string targetId, string key, string value)
        {
            this.TargetId = targetId;
            this.Key = key;
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
