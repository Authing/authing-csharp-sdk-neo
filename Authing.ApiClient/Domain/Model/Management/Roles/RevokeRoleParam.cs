using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class RevokeRoleParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("roleCode")]
        public string RoleCode { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("roleCodes")]
        public IEnumerable<string> RoleCodes { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("userIds")]
        public IEnumerable<string> UserIds { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("groupCodes")]
        public IEnumerable<string> GroupCodes { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("nodeCodes")]
        public IEnumerable<string> NodeCodes { get; set; }

        public RevokeRoleParam()
        {

        }
        /// <summary>
        /// RevokeRoleParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { namespace=(string), roleCode=(string), roleCodes=(string[]), userIds=(string[]), groupCodes=(string[]), nodeCodes=(string[]) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RevokeRoleDocument,
                OperationName = "revokeRole",
                Variables = this
            };
        }


        public static string RevokeRoleDocument = @"
        mutation revokeRole($namespace: String, $roleCode: String, $roleCodes: [String], $userIds: [String!], $groupCodes: [String!], $nodeCodes: [String!]) {
          revokeRole(namespace: $namespace, roleCode: $roleCode, roleCodes: $roleCodes, userIds: $userIds, groupCodes: $groupCodes, nodeCodes: $nodeCodes) {
            message
            code
          }
        }
        ";
    }
}
