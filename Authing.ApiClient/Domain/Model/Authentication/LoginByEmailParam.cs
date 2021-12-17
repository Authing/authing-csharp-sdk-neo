using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class LoginByEmailParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("input")]
        public LoginByEmailInput Input { get; set; }

        public LoginByEmailParam(LoginByEmailInput input)
        {
            this.Input = input;
        }
        /// <summary>
        /// LoginByEmailParam.Request 
        /// <para>Required variables:<br/> { input=(LoginByEmailInput) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = LoginByEmailDocument,
                OperationName = "loginByEmail",
                Variables = this
            };
        }


        public static string LoginByEmailDocument = @"
        mutation loginByEmail($input: LoginByEmailInput!) {
          loginByEmail(input: $input) {
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
          }
        }
        ";
    }
}
