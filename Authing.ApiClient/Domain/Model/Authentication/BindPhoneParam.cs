using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class BindPhoneParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("phoneCode")]
        public string PhoneCode { get; set; }

        public BindPhoneParam(string phone, string phoneCode)
        {
            this.Phone = phone;
            this.PhoneCode = phoneCode;
        }
        /// <summary>
        /// BindPhoneParam.Request 
        /// <para>Required variables:<br/> { phone=(string), phoneCode=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = BindPhoneDocument,
                OperationName = "bindPhone",
                Variables = this
            };
        }


        public static string BindPhoneDocument = @"
        mutation bindPhone($phone: String!, $phoneCode: String!) {
          bindPhone(phone: $phone, phoneCode: $phoneCode) {
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
