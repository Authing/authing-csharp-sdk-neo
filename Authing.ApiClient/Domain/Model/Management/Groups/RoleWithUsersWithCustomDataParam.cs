using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class RoleWithUsersWithCustomDataParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

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

        public RoleWithUsersWithCustomDataParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// RoleWithUsersWithCustomDataParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { namespace=(string), page=(int), limit=(int) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RoleWithUsersWithCustomDataDocument,
                OperationName = "roleWithUsersWithCustomData",
                Variables = this
            };
        }


        public static string RoleWithUsersWithCustomDataDocument = @"
        query roleWithUsersWithCustomData($code: String!, $namespace: String, $page: Int, $limit: Int) {
          role(code: $code, namespace: $namespace) {
            users(page: $page, limit: $limit) {
              totalCount
              list {
                id
                arn
                status
                userPoolId
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
                customData {
                  key
                  value
                  dataType
                  label
                }
              }
            }
          }
        }
        ";
    }
}