using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Department
{
    public class GetUserDepartmentsParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        public GetUserDepartmentsParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// GetUserDepartmentsParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { orgId=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GetUserDepartmentsDocument,
                OperationName = "getUserDepartments",
                Variables = this
            };
        }


        public static string GetUserDepartmentsDocument = @"
        query getUserDepartments($id: String!, $orgId: String) {
          user(id: $id) {
            departments(orgId: $orgId) {
              totalCount
              list {
                department {
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
                  codePath
                  namePath
                  createdAt
                  updatedAt
                  children
                }
                isMainDepartment
                joinedAt
              }
            }
          }
        }
        ";
    }
}
