using Authing.ApiClient.Domain.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UserDefinedDataInput
    {
        #region members
        [JsonProperty("key")]
        [JsonRequired]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        #endregion


        /// <summary>
        /// <param name="key">key</param>
        /// </summary>

        public UserDefinedDataInput(string key)
        {
            this.Key = key;
        }

        #region methods
        public dynamic GetInputObject()
        {
            return ReflectionHelper.GetInputObjec(this);
        }
        #endregion
    }
}
