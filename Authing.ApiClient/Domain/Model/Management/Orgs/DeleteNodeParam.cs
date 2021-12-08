using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class DeleteNodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        public DeleteNodeParam(string orgId, string nodeId)
        {
            this.OrgId = orgId;
            this.NodeId = nodeId;
        }
        /// <summary>
        /// DeleteNodeParam.Request 
        /// <para>Required variables:<br/> { orgId=(string), nodeId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteNodeDocument,
                OperationName = "deleteNode",
                Variables = this
            };
        }


        public static string DeleteNodeDocument = @"
        mutation deleteNode($orgId: String!, $nodeId: String!) {
          deleteNode(orgId: $orgId, nodeId: $nodeId) {
            message
            code
          }
        }
        ";
    }
}
