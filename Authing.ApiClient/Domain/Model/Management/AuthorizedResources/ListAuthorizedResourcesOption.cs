using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
namespace Authing.ApiClient.Domain.Model.Management.AuthorizedResources
{
    public class ListAuthorizedResourcesOption
    {
        public ResourceType? ResourceType { get; set; }
    }
}
