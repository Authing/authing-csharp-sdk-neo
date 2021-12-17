using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class LoginBySubAccountParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("captchaCode")]
        public string CaptchaCode { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("clientIp")]
        public string ClientIp { get; set; }

        public LoginBySubAccountParam(string account, string password)
        {
            this.Account = account;
            this.Password = password;
        }
        /// <summary>
        /// LoginBySubAccountParam.Request 
        /// <para>Required variables:<br/> { account=(string), password=(string) }</para>
        /// <para>Optional variables:<br/> { captchaCode=(string), clientIp=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = LoginBySubAccountDocument,
                OperationName = "loginBySubAccount",
                Variables = this
            };
        }


        public static string LoginBySubAccountDocument = @"
        mutation loginBySubAccount($account: String!, $password: String!, $captchaCode: String, $clientIp: String) {
          loginBySubAccount(account: $account, password: $password, captchaCode: $captchaCode, clientIp: $clientIp) {
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
