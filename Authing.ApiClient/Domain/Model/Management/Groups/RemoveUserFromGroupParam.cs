using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class RemoveUserFromGroupParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userIds")]
        public IEnumerable<string> UserIds { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        public RemoveUserFromGroupParam(IEnumerable<string> userIds)
        {
            this.UserIds = userIds;
        }
        /// <summary>
        /// RemoveUserFromGroupParam.Request 
        /// <para>Required variables:<br/> { userIds=(string[]) }</para>
        /// <para>Optional variables:<br/> { code=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RemoveUserFromGroupDocument,
                OperationName = "removeUserFromGroup",
                Variables = this
            };
        }


        public static string RemoveUserFromGroupDocument = @"
        mutation removeUserFromGroup($userIds: [String!]!, $code: String) {
          removeUserFromGroup(userIds: $userIds, code: $code) {
            message
            code
          }
        }
        ";
    }
}