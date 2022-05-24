using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class DeleteUserParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public DeleteUserParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// DeleteUserParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = DeleteUserDocument,
                OperationName = "deleteUser",
                Variables = this
            };
        }


        public static string DeleteUserDocument = @"
        mutation deleteUser($id: String!) {
          deleteUser(id: $id) {
            message
            code
          }
        }
        ";
    }
}