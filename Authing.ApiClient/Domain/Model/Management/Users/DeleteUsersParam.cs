using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class DeleteUsersParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("ids")]
        public IEnumerable<string> Ids { get; set; }

        public DeleteUsersParam(IEnumerable<string> ids)
        {
            this.Ids = ids;
        }
        /// <summary>
        /// DeleteUsersParam.Request 
        /// <para>Required variables:<br/> { ids=(string[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteUsersDocument,
                OperationName = "deleteUsers",
                Variables = this
            };
        }


        public static string DeleteUsersDocument = @"
        mutation deleteUsers($ids: [String!]!) {
          deleteUsers(ids: $ids) {
            message
            code
          }
        }
        ";
    }
}