using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class SetMainDepartmentParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }

        public SetMainDepartmentParam(string userId)
        {
            this.UserId = userId;
        }
        /// <summary>
        /// SetMainDepartmentParam.Request 
        /// <para>Required variables:<br/> { userId=(string) }</para>
        /// <para>Optional variables:<br/> { departmentId=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SetMainDepartmentDocument,
                OperationName = "setMainDepartment",
                Variables = this
            };
        }


        public static string SetMainDepartmentDocument = @"
        mutation setMainDepartment($userId: String!, $departmentId: String) {
          setMainDepartment(userId: $userId, departmentId: $departmentId) {
            message
            code
          }
        }
        ";
    }
}
