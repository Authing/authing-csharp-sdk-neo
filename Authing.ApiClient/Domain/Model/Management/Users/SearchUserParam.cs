using System.Collections.Generic;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class SearchUserParam
    {
        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("fields")]
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("departmentOpts")]
        public IEnumerable<SearchUserDepartmentOpt> DepartmentOpts { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("groupOpts")]
        public IEnumerable<SearchUserGroupOpt> GroupOpts { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("roleOpts")]
        public IEnumerable<SearchUserRoleOpt> RoleOpts { get; set; }

        public SearchUserParam(string query)
        {
            this.Query = query;
        }

        /// <summary>
        /// SearchUserParam.Request
        /// <para>Required variables:<br/> { query=(string) }</para>
        /// <para>Optional variables:<br/> { fields=(string[]), page=(int), limit=(int), departmentOpts=(SearchUserDepartmentOpt[]), groupOpts=(SearchUserGroupOpt[]), roleOpts=(SearchUserRoleOpt[]) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SearchUserDocument,
                OperationName = "searchUser",
                Variables = this
            };
        }

        public static string SearchUserDocument = @"
        query searchUser($query: String!, $fields: [String], $page: Int, $limit: Int, $departmentOpts: [SearchUserDepartmentOpt], $groupOpts: [SearchUserGroupOpt], $roleOpts: [SearchUserRoleOpt]) {
          searchUser(query: $query, fields: $fields, page: $page, limit: $limit, departmentOpts: $departmentOpts, groupOpts: $groupOpts, roleOpts: $roleOpts) {
            totalCount
            list {
              id
              arn
              userPoolId
              status
              username
              email
              emailVerified
              phone
              phoneVerified
              unionid
              openid
              nickname
              registerSource
              photo
              password
              oauth
              token
              tokenExpiredAt
              loginsCount
              lastLogin
              lastIP
              signedUp
              blocked
              isDeleted
              device
              browser
              company
              name
              givenName
              familyName
              middleName
              profile
              preferredUsername
              website
              gender
              birthdate
              zoneinfo
              locale
              address
              formatted
              streetAddress
              locality
              region
              postalCode
              city
              province
              country
              createdAt
              updatedAt
              externalId
            }
          }
        }
        ";
    }
}