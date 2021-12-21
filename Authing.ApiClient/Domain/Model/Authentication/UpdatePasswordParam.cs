using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class UpdatePasswordParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }

        public UpdatePasswordParam(string newPassword)
        {
            this.NewPassword = newPassword;
        }
        /// <summary>
        /// UpdatePasswordParam.Request 
        /// <para>Required variables:<br/> { newPassword=(string) }</para>
        /// <para>Optional variables:<br/> { oldPassword=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdatePasswordDocument,
                OperationName = "updatePassword",
                Variables = this
            };
        }


        public static string UpdatePasswordDocument = @"
        mutation updatePassword($newPassword: String!, $oldPassword: String) {
          updatePassword(newPassword: $newPassword, oldPassword: $oldPassword) {
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
