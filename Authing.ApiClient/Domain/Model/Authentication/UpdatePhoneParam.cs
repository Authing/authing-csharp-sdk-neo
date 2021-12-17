using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class UpdatePhoneParam
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

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("oldPhone")]
        public string OldPhone { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("oldPhoneCode")]
        public string OldPhoneCode { get; set; }

        public UpdatePhoneParam(string phone, string phoneCode)
        {
            this.Phone = phone;
            this.PhoneCode = phoneCode;
        }
        /// <summary>
        /// UpdatePhoneParam.Request 
        /// <para>Required variables:<br/> { phone=(string), phoneCode=(string) }</para>
        /// <para>Optional variables:<br/> { oldPhone=(string), oldPhoneCode=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdatePhoneDocument,
                OperationName = "updatePhone",
                Variables = this
            };
        }


        public static string UpdatePhoneDocument = @"
        mutation updatePhone($phone: String!, $phoneCode: String!, $oldPhone: String, $oldPhoneCode: String) {
          updatePhone(phone: $phone, phoneCode: $phoneCode, oldPhone: $oldPhone, oldPhoneCode: $oldPhoneCode) {
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
