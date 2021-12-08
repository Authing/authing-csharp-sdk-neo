using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Extensions
{
    public static class ObjectConvertJson
    {
        public static string ConvertJson(this object _object)
        {
            var resObj = JsonConvert.SerializeObject(_object);
            return resObj;
        }
    }
}
