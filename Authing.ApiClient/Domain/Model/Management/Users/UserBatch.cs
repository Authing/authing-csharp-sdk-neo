using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class UserBatchResponse
    {

        [JsonProperty("userBatch")]
        public IEnumerable<User> Result { get; set; }
    }

    public class UserBatchParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("ids")]
        public IEnumerable<string> Ids { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        public UserBatchParam(IEnumerable<string> ids)
        {
            this.Ids = ids;
        }
        /// <summary>
        /// UserBatchParam.Request 
        /// <para>Required variables:<br/> { ids=(string[]) }</para>
        /// <para>Optional variables:<br/> { type=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UserBatchDocument,
                OperationName = "userBatch",
                Variables = this
            };
        }


        public static string UserBatchDocument = @"
        query userBatch($ids: [String!]!, $type: String) {
          userBatch(ids: $ids, type: $type) {
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
