using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Authing.ApiClient.Domain.Utils;

namespace Authing.ApiClient.Extensions
{
    public static class Anonymous2QueryParams
    {
        public static string Convert2QueryParams(this object o)
        {
            if (!CheckIfAnonymousType(o.GetType())) throw new ArgumentException("This param type not AnonymousType");
            var result = ReflectionHelper.GetInputObjec(o);
            var querystring = "?";
            foreach (var data in result)
            {
                querystring += $"{data.Key.ToLower()}={data.Value}&";
            }
            return querystring.Remove(querystring.Length - 1, 1);
        }

        private static bool CheckIfAnonymousType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>"))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }
}
