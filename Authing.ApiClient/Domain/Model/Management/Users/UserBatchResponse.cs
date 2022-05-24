using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class UserBatchResponse
    {

        [JsonProperty("userBatch")]
        public IEnumerable<User> Result { get; set; }
    }
}
