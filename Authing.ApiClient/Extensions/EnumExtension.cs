using System;
using System.ComponentModel;
using System.Linq;

namespace Authing.ApiClient.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescription(this Enum val)
        {
            var type = val.GetType();

            var memberInfo = type.GetMember(val.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length != 1)
            {
                //如果没有定义描述，就把当前枚举值的对应名称返回
                return val.ToString();
            }

            return (attributes.Single() as DescriptionAttribute)?.Description;
        }
    }
}
