using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class UserDdfInput
    {
        #region members
        [JsonProperty("key")]
        [JsonRequired]
        public string Key { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public string Value { get; set; }
        #endregion


        /// <summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// </summary>

        public UserDdfInput(string key, string value)
        {
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
