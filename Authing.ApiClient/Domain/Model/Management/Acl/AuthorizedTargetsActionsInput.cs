using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AuthorizedTargetsActionsInput
    {
        #region members
        [JsonProperty("op")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonRequired]
        public Operator Op { get; set; }

        [JsonProperty("list")]
        [JsonRequired]
        public IEnumerable<string> List { get; set; }
        #endregion


        /// <summary>
        /// <param name="op">op</param>
        /// <param name="list">list</param>
        /// </summary>

        public AuthorizedTargetsActionsInput(Operator op, IEnumerable<string> list)
        {
            this.Op = op;
            this.List = list;
        }

        #region methods
        //public dynamic GetInputObject()
        //{
        //    IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

        //    var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        //    foreach (var propertyInfo in properties)
        //    {
        //        var value = propertyInfo.GetValue(this);
        //        var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

        //        var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
        //        if (requiredProp || value != defaultValue)
        //        {
        //            d[propertyInfo.Name] = value;
        //        }
        //    }
        //    return d;
        //}
        #endregion
    }
}