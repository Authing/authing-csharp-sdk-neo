using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UpdateUserParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("input")]
        public UpdateUserInput Input { get; set; }

        public UpdateUserParam(UpdateUserInput input)
        {
            this.Input = input;
        }
        /// <summary>
        /// UpdateUserParam.Request 
        /// <para>Required variables:<br/> { input=(UpdateUserInput) }</para>
        /// <para>Optional variables:<br/> { id=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateUserDocument,
                OperationName = "updateUser",
                Variables = this
            };
        }


        public static string UpdateUserDocument = @"
        mutation updateUser($id: String, $input: UpdateUserInput!) {
          updateUser(id: $id, input: $input) {
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