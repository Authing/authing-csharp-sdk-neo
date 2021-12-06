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
    public class RemoveWhitelistParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WhitelistType Type { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<string> List { get; set; }

        public RemoveWhitelistParam(WhitelistType type, IEnumerable<string> list)
        {
            this.Type = type;
            this.List = list;
        }
        /// <summary>
        /// RemoveWhitelistParam.Request 
        /// <para>Required variables:<br/> { type=(WhitelistType), list=(string[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RemoveWhitelistDocument,
                OperationName = "removeWhitelist",
                Variables = this
            };
        }


        public static string RemoveWhitelistDocument = @"
        mutation removeWhitelist($type: WhitelistType!, $list: [String!]!) {
          removeWhitelist(type: $type, list: $list) {
            createdAt
            updatedAt
            value
          }
        }
        ";
    }

}
