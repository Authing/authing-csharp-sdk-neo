using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class FindUserParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("identity")]
        public FindUserByIdentityInput Identity { get; set; }

        public FindUserParam()
        {

        }
        /// <summary>
        /// FindUserParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { email=(string), phone=(string), username=(string), externalId=(string), identity=(FindUserByIdentityInput) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = FindUserDocument,
                OperationName = "findUser",
                Variables = this
            };
        }


        public static string FindUserDocument = @"
        query findUser($email: String, $phone: String, $username: String, $externalId: String, $identity: FindUserByIdentityInput) {
          findUser(email: $email, phone: $phone, username: $username, externalId: $externalId, identity: $identity) {
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
        ";
    }
}