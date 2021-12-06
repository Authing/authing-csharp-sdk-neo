using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class IsRootNodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        public IsRootNodeParam(string nodeId, string orgId)
        {
            this.NodeId = nodeId;
            this.OrgId = orgId;
        }
        /// <summary>
        /// IsRootNodeParam.Request 
        /// <para>Required variables:<br/> { nodeId=(string), orgId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = IsRootNodeDocument,
                OperationName = "isRootNode",
                Variables = this
            };
        }


        public static string IsRootNodeDocument = @"
        query isRootNode($nodeId: String!, $orgId: String!) {
          isRootNode(nodeId: $nodeId, orgId: $orgId)
        }
        ";
    }
}
