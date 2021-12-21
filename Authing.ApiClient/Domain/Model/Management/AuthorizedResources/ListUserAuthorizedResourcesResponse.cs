using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.AuthorizedResources
{
    public class ListUserAuthorizedResourcesResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }
}
