using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class DeleteGroupsParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("codeList")]
        public IEnumerable<string> CodeList { get; set; }

        public DeleteGroupsParam(IEnumerable<string> codeList)
        {
            this.CodeList = codeList;
        }
        /// <summary>
        /// DeleteGroupsParam.Request 
        /// <para>Required variables:<br/> { codeList=(string[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteGroupsDocument,
                OperationName = "deleteGroups",
                Variables = this
            };
        }


        public static string DeleteGroupsDocument = @"
        mutation deleteGroups($codeList: [String!]!) {
          deleteGroups(codeList: $codeList) {
            message
            code
          }
        }
        ";
    }
}
