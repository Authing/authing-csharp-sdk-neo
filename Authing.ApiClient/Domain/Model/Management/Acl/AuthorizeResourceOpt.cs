using System;
using System.Collections.Generic;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class AuthorizeResourceOpt
    {
        #region members
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonRequired]
        public PolicyAssignmentTargetType TargetType { get; set; }

        [JsonProperty("targetIdentifier")]
        [JsonRequired]
        public string TargetIdentifier { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<string> Actions { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifier"></param>
        /// <param name="actions"></param>
        public AuthorizeResourceOpt(PolicyAssignmentTargetType targetType, string targetIdentifier, IEnumerable<string> actions = null)
        {
            TargetType = targetType;
            TargetIdentifier = targetIdentifier;
            Actions = actions;
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