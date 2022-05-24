using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class DeleteUserResponse
    {

        [JsonProperty("deleteUser")]
        public CommonMessage Result { get; set; }
    }
}
