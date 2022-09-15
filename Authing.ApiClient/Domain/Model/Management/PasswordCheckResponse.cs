using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.Management
{
    /// <summary>
    /// 密码认证响应数据
    /// </summary>
    public class PasswordCheckResponse
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public string ErrorType { get; set; }
    }
}
