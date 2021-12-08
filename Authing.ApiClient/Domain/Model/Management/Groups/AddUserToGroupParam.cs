using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class AddUserToGroupParam
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

        public AddUserToGroupParam(IEnumerable<string> userIds)
        {
            this.UserIds = userIds;
        }
        /// <summary>
        /// AddUserToGroupParam.Request 
        /// <para>Required variables:<br/> { userIds=(string[]) }</para>
        /// <para>Optional variables:<br/> { code=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AddUserToGroupDocument,
                OperationName = "addUserToGroup",
                Variables = this
            };
        }


        public static string AddUserToGroupDocument = @"
        mutation addUserToGroup($userIds: [String!]!, $code: String) {
          addUserToGroup(userIds: $userIds, code: $code) {
            message
            code
          }
        }
        ";
    }
}