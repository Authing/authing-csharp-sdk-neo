using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class GetUserGroupsParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public GetUserGroupsParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// GetUserGroupsParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GetUserGroupsDocument,
                OperationName = "getUserGroups",
                Variables = this
            };
        }


        public static string GetUserGroupsDocument = @"
        query getUserGroups($id: String!) {
          user(id: $id) {
            groups {
              totalCount
              list {
                code
                name
                description
                createdAt
                updatedAt
              }
            }
          }
        }
        ";
    }
}