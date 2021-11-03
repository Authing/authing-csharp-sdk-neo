using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UserParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public UserParam()
        {

        }
        /// <summary>
        /// UserParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { id=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UserDocument,
                OperationName = "user",
                Variables = this
            };
        }


        public static string UserDocument = @"
        query user($id: String) {
          user(id: $id) {
            id
            arn
            userPoolId
            status
            username
            email
            emailVerified
            phone
            phoneVerified
            identities {
              openid
              userIdInIdp
              userId
              connectionId
              isSocial
              provider
              userPoolId
            }
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
        ";
    }
}