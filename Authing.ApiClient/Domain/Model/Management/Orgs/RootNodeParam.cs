using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class RootNodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        public RootNodeParam(string orgId)
        {
            this.OrgId = orgId;
        }
        /// <summary>
        /// RootNodeParam.Request 
        /// <para>Required variables:<br/> { orgId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RootNodeDocument,
                OperationName = "rootNode",
                Variables = this
            };
        }


        public static string RootNodeDocument = @"
        query rootNode($orgId: String!) {
          rootNode(orgId: $orgId) {
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
            codePath
            namePath
            createdAt
            updatedAt
            children
          }
        }
        ";
    }
}
