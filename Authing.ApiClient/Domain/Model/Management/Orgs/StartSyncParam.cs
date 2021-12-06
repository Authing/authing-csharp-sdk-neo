using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class StartSyncParam
    {
        [JsonProperty("connectionId")]
        public string ConnectionId { get; set; }

        public ProviderTypeEnum ProviderTypeEnum { get; set; }

        public StartSyncParam(ProviderTypeEnum providerTypeEnum)
        {
            this.ProviderTypeEnum = providerTypeEnum;
        }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest 
            {
                
            };
        }
    }
}
