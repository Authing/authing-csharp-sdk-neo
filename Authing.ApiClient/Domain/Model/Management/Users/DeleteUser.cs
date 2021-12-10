using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class DeleteUserResponse
    {

        [JsonProperty("deleteUser")]
        public CommonMessage Result { get; set; }
    }

    public class DeleteUsersResponse
    {

        [JsonProperty("deleteUsers")]
        public CommonMessage Result { get; set; }
    }

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
