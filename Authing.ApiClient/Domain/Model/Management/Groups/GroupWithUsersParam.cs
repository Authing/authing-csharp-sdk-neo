using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class GroupWithUsersParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

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

        public GroupWithUsersParam(string code)
        {
            this.Code = code;
        }
        /// <summary>
        /// GroupWithUsersParam.Request 
        /// <para>Required variables:<br/> { code=(string) }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = GroupWithUsersDocument,
                OperationName = "groupWithUsers",
                Variables = this
            };
        }


        public static string GroupWithUsersDocument = @"
        query groupWithUsers($code: String!, $page: Int, $limit: Int) {
          group(code: $code) {
            users(page: $page, limit: $limit) {
              totalCount
              list {
                id
                arn
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
              }
            }
          }
        }
        ";
    }
}