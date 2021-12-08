using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class MoveNodeParam
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

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetParentId")]
        public string TargetParentId { get; set; }

        public MoveNodeParam(string orgId, string nodeId, string targetParentId)
        {
            this.OrgId = orgId;
            this.NodeId = nodeId;
            this.TargetParentId = targetParentId;
        }
        /// <summary>
        /// MoveNodeParam.Request 
        /// <para>Required variables:<br/> { orgId=(string), nodeId=(string), targetParentId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = MoveNodeDocument,
                OperationName = "moveNode",
                Variables = this
            };
        }


        public static string MoveNodeDocument = @"
        mutation moveNode($orgId: String!, $nodeId: String!, $targetParentId: String!) {
          moveNode(orgId: $orgId, nodeId: $nodeId, targetParentId: $targetParentId) {
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
