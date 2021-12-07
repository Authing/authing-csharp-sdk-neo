using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class UpdateGroupParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("newCode")]
        public string NewCode { get; set; }

        public UpdateGroupParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// UpdateGroupParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { name=(string), description=(string), newCode=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateGroupDocument,
                OperationName = "updateGroup",
                Variables = this
            };
        }


        public static string UpdateGroupDocument = @"
        mutation updateGroup($code: String!, $name: String, $description: String, $newCode: String) {
          updateGroup(code: $code, name: $name, description: $description, newCode: $newCode) {
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
