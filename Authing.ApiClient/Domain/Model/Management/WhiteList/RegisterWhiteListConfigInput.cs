using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.WhiteList
{
    public class RegisterWhiteListConfigInput
    {
        #region members
        [JsonProperty("phoneEnabled")]
        public bool? PhoneEnabled { get; set; }

        [JsonProperty("emailEnabled")]
        public bool? EmailEnabled { get; set; }

        [JsonProperty("usernameEnabled")]
        public bool? UsernameEnabled { get; set; }
        #endregion

        public RegisterWhiteListConfigInput()
        {

        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }

}
