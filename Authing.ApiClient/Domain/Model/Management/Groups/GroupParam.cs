using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class GroupParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        public GroupParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// GroupParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GroupDocument,
                OperationName = "group",
                Variables = this
            };
        }


        public static string GroupDocument = @"
        query group($code: String!) {
          group(code: $code) {
            code
            name
            description
            createdAt
            updatedAt
          }
        }
        ";
    }

}
