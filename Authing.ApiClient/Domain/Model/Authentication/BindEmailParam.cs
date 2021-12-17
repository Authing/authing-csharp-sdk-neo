using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class BindEmailParam
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

        public BindEmailParam(string email, string emailCode)
        {
            this.Email = email;
            this.EmailCode = emailCode;
        }
        /// <summary>
        /// BindEmailParam.Request 
        /// <para>Required variables:<br/> { email=(string), emailCode=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = BindEmailDocument,
                OperationName = "bindEmail",
                Variables = this
            };
        }


        public static string BindEmailDocument = @"
        mutation bindEmail($email: String!, $emailCode: String!) {
          bindEmail(email: $email, emailCode: $emailCode) {
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
