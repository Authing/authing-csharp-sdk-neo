using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class DeleteOrgParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public DeleteOrgParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// DeleteOrgParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteOrgDocument,
                OperationName = "deleteOrg",
                Variables = this
            };
        }


        public static string DeleteOrgDocument = @"
        mutation deleteOrg($id: String!) {
          deleteOrg(id: $id) {
            message
            code
          }
        }
        ";
    }
}
