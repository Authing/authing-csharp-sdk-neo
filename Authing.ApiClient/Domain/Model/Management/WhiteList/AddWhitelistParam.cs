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
    public class AddWhitelistParam
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

        public AddWhitelistParam(WhitelistType type, IEnumerable<string> list)
        {
            this.Type = type;
            this.List = list;
        }
        /// <summary>
        /// AddWhitelistParam.Request 
        /// <para>Required variables:<br/> { type=(WhitelistType), list=(string[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AddWhitelistDocument,
                OperationName = "addWhitelist",
                Variables = this
            };
        }


        public static string AddWhitelistDocument = @"
        mutation addWhitelist($type: WhitelistType!, $list: [String!]!) {
          addWhitelist(type: $type, list: $list) {
            createdAt
            updatedAt
            value
          }
        }
        ";
    }
}
