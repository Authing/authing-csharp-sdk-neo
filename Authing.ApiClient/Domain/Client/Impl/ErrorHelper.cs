using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Client.Impl
{
    public class ErrorHelper
    {
        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="error"></param>
        /// <param name="authingErrorBox"></param>
        public static void LoadError<T>(GraphQLResponse<T> response, AuthingErrorBox authingErrorBox = null)
        {
            authingErrorBox?.Clear();
            if (response.Errors != null && response.Errors.Length > 0)
            {
                authingErrorBox.Set(response.Errors);
            }
        }
    }
}
