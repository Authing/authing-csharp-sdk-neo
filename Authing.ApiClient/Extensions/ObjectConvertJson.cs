using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Authing.ApiClient.Extensions
{
    public static class ObjectConvertJson
    {
        public static string ConvertJson(this object _object)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                // 设置为驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var resObj = JsonConvert.SerializeObject(_object,settings:serializerSettings);
            return resObj;
        }
    }
}
