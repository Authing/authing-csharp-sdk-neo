using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class OrgParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public OrgParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// OrgParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = OrgDocument,
                OperationName = "org",
                Variables = this
            };
        }


        public static string OrgDocument = @"
        query org($id: String!) {
          org(id: $id) {
            id
            rootNode {
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
            nodes {
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
        }
        ";
    }
}
