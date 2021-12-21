using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class RegisterByPhoneCodeParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("input")]
        public RegisterByPhoneCodeInput Input { get; set; }

        public RegisterByPhoneCodeParam(RegisterByPhoneCodeInput input)
        {
            this.Input = input;
        }
        /// <summary>
        /// RegisterByPhoneCodeParam.Request 
        /// <para>Required variables:<br/> { input=(RegisterByPhoneCodeInput) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RegisterByPhoneCodeDocument,
                OperationName = "registerByPhoneCode",
                Variables = this
            };
        }


        public static string RegisterByPhoneCodeDocument = @"
        mutation registerByPhoneCode($input: RegisterByPhoneCodeInput!) {
          registerByPhoneCode(input: $input) {
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
