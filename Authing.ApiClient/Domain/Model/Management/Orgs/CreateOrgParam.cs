using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class CreateOrgParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        public CreateOrgParam(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// CreateOrgParam.Request 
        /// <para>Required variables:<br/> { name=(string) }</para>
        /// <para>Optional variables:<br/> { code=(string), description=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CreateOrgDocument,
                OperationName = "createOrg",
                Variables = this
            };
        }


        public static string CreateOrgDocument = @"
        mutation createOrg($name: String!, $code: String, $description: String) {
          createOrg(name: $name, code: $code, description: $description) {
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
