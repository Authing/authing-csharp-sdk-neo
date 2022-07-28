using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.V3Model
{
    public class CommonResponse<T>
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public int apiCode { get; set; }
        public T data { get; set; }
    }
}
