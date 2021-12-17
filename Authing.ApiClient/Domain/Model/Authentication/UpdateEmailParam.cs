using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class UpdateEmailParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("emailCode")]
        public string EmailCode { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("oldEmail")]
        public string OldEmail { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("oldEmailCode")]
        public string OldEmailCode { get; set; }

        public UpdateEmailParam(string email, string emailCode)
        {
            this.Email = email;
            this.EmailCode = emailCode;
        }
        /// <summary>
        /// UpdateEmailParam.Request 
        /// <para>Required variables:<br/> { email=(string), emailCode=(string) }</para>
        /// <para>Optional variables:<br/> { oldEmail=(string), oldEmailCode=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateEmailDocument,
                OperationName = "updateEmail",
                Variables = this
            };
        }


        public static string UpdateEmailDocument = @"
        mutation updateEmail($email: String!, $emailCode: String!, $oldEmail: String, $oldEmailCode: String) {
          updateEmail(email: $email, emailCode: $emailCode, oldEmail: $oldEmail, oldEmailCode: $oldEmailCode) {
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
          }
        }
        ";
    }
}
