using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Authing.ApiClient.Extensions
{
    public  static class ResourceVarCheackExt
    {
        public static bool CheckParameter(this string parameter)
        {
            Regex temp = new Regex(@"^[0-9a-z]+:[0-9a-z*]+$", RegexOptions.IgnoreCase);
            if (!(temp.IsMatch(parameter))) throw new ArgumentException($"parameter {parameter} not correct !");
            return true;
        }
    }
}
