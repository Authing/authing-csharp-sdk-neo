using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class LoginByPhoneCodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("input")]
        public LoginByPhoneCodeInput Input { get; set; }

        public LoginByPhoneCodeParam(LoginByPhoneCodeInput input)
        {
            this.Input = input;
        }
        /// <summary>
        /// LoginByPhoneCodeParam.Request 
        /// <para>Required variables:<br/> { input=(LoginByPhoneCodeInput) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = LoginByPhoneCodeDocument,
                OperationName = "loginByPhoneCode",
                Variables = this
            };
        }


        public static string LoginByPhoneCodeDocument = @"
        mutation loginByPhoneCode($input: LoginByPhoneCodeInput!) {
          loginByPhoneCode(input: $input) {
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
