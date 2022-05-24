using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class CreateUserParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userInfo")]
        public CreateUserInput UserInfo { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("params")]
        public string Params { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("identity")]
        public CreateUserIdentityInput Identity { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("keepPassword")]
        public bool? KeepPassword { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("resetPasswordOnFirstLogin")]
        public bool? ResetPasswordOnFirstLogin { get; set; }

        public CreateUserParam(CreateUserInput userInfo)
        {
            this.UserInfo = userInfo;
        }
        /// <summary>
        /// CreateUserParam.Request 
        /// <para>Required variables:<br/> { userInfo=(CreateUserInput) }</para>
        /// <para>Optional variables:<br/> { params=(string), identity=(CreateUserIdentityInput), keepPassword=(bool), resetPasswordOnFirstLogin=(bool) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CreateUserDocument,
                OperationName = "createUser",
                Variables = this
            };
        }


        public static string CreateUserDocument = @"
        mutation createUser($userInfo: CreateUserInput!, $params: String, $identity: CreateUserIdentityInput, $keepPassword: Boolean, $resetPasswordOnFirstLogin: Boolean) {
          createUser(userInfo: $userInfo, params: $params, identity: $identity, keepPassword: $keepPassword, resetPasswordOnFirstLogin: $resetPasswordOnFirstLogin) {
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