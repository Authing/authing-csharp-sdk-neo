using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class MoveMembersParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userIds")]
        public IEnumerable<string> UserIds { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("sourceNodeId")]
        public string SourceNodeId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetNodeId")]
        public string TargetNodeId { get; set; }

        public MoveMembersParam(IEnumerable<string> userIds, string sourceNodeId, string targetNodeId)
        {
            this.UserIds = userIds;
            this.SourceNodeId = sourceNodeId;
            this.TargetNodeId = targetNodeId;
        }
        /// <summary>
        /// MoveMembersParam.Request 
        /// <para>Required variables:<br/> { userIds=(string[]), sourceNodeId=(string), targetNodeId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = MoveMembersDocument,
                OperationName = "moveMembers",
                Variables = this
            };
        }


        public static string MoveMembersDocument = @"
        mutation moveMembers($userIds: [String!]!, $sourceNodeId: String!, $targetNodeId: String!) {
          moveMembers(userIds: $userIds, sourceNodeId: $sourceNodeId, targetNodeId: $targetNodeId) {
            code
            message
          }
        }
        ";
    }
}
