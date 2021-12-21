using System;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class SetUserUdfValueBatchParam
    {
        public string UserId { get; set; }
        public KeyValueDictionary Data { get; set; }
    }
}
