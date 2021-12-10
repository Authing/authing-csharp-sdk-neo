using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class GetUserRolesResponse
    {

        [JsonProperty("user")]
        public User Result { get; set; }
    }

    public class GetUserRolesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        public GetUserRolesParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// GetUserRolesParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GetUserRolesDocument,
                OperationName = "getUserRoles",
                Variables = this
            };
        }


        public static string GetUserRolesDocument = @"
        query getUserRoles($id: String!, $namespace: String) {
          user(id: $id) {
            roles(namespace: $namespace) {
              totalCount
              list {
                id
                code
                namespace
                arn
                description
                createdAt
                updatedAt
                parent {
                  code
                  namespace
                  arn
                  description
                  createdAt
                  updatedAt
                }
              }
            }
          }
        }
        ";
    }
}
