using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.WhiteList
{
    public class WhitelistParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WhitelistType Type { get; set; }

        public WhitelistParam(WhitelistType type)
        {
            this.Type = type;
        }
        /// <summary>
        /// WhitelistParam.Request 
        /// <para>Required variables:<br/> { type=(WhitelistType) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = WhitelistDocument,
                OperationName = "whitelist",
                Variables = this
            };
        }


        public static string WhitelistDocument = @"
        query whitelist($type: WhitelistType!) {
          whitelist(type: $type) {
            createdAt
            updatedAt
            value
          }
        }
        ";
    }

}