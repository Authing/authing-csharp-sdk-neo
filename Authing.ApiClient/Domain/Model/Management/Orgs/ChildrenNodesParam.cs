using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class ChildrenNodesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        public ChildrenNodesParam(string nodeId)
        {
            this.NodeId = nodeId;
        }
        /// <summary>
        /// ChildrenNodesParam.Request 
        /// <para>Required variables:<br/> { nodeId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ChildrenNodesDocument,
                OperationName = "childrenNodes",
                Variables = this
            };
        }


        public static string ChildrenNodesDocument = @"
        query childrenNodes($nodeId: String!) {
          childrenNodes(nodeId: $nodeId) {
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
