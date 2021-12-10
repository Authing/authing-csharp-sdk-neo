using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class SearchOption
    {
        public string[]? Fields { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 10;

        public SearchUserDepartmentOpt[] DepartmentOpts { get; set; }

        // public IEnumerable<SearchUserDepartmentOpt>? DepartmentOpts { get; set; }
        public SearchUserGroupOpt[] GroupOpts { get; set; }

        public SearchUserRoleOpt[] RoleOpts { get; set; }

        public bool WithCustomData { get; set; }


    }

    public class SearchUserResponse
    {

        [JsonProperty("searchUser")]
        public PaginatedUsers Result { get; set; }
    }

    #region SearchUserDepartmentOpt
    public class SearchUserDepartmentOpt
    {
        #region members
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }

        [JsonProperty("includeChildrenDepartments")]
        public bool? IncludeChildrenDepartments { get; set; }
        #endregion



        public SearchUserDepartmentOpt()
        {

        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }
    #endregion

    #region SearchUserGroupOpt
    public class SearchUserGroupOpt
    {
        #region members
        [JsonProperty("code")]
        public string Code { get; set; }
        #endregion



        public SearchUserGroupOpt()
        {

        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }
    #endregion

    #region SearchUserRoleOpt
    public class SearchUserRoleOpt
    {
        #region members
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("code")]
        [JsonRequired]
        public string Code { get; set; }
        #endregion


        /// <summary>
        /// <param name="code">code</param>
        /// </summary>

        public SearchUserRoleOpt(string code)
        {
            this.Code = code;
        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }
    #endregion

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
