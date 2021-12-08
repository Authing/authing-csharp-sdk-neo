using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class NodeByIdParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public NodeByIdParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// NodeByIdParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = NodeByIdDocument,
                OperationName = "nodeById",
                Variables = this
            };
        }


        public static string NodeByIdDocument = @"
        query nodeById($id: String!) {
          nodeById(id: $id) {
            id
            orgId
            name
            nameI18n
            description
            descriptionI18n
            order
            code
            root
            depth
            path
            createdAt
            updatedAt
            children
          }
        }
        ";
    }
}
