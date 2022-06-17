using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Authing.Library.Extensions
{
    public static class EnumExtension
    {
        public static string? GetEnumMemberValue<T>(this T value) where T : Enum
        {
            var enumType = typeof(T);
            var memInfo = enumType.GetMember(value.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null)
            {
                return attr.Value;
            }
            return "";
        }
    }
}