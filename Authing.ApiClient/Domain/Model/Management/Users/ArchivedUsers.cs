using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class ArchivedUsersResponse
    {

        [JsonProperty("archivedUsers")]
        public PaginatedUsers Result { get; set; }
    }

    public class ArchivedUsersParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        public ArchivedUsersParam()
        {

        }
        /// <summary>
        /// ArchivedUsersParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ArchivedUsersDocument,
                OperationName = "archivedUsers",
                Variables = this
            };
        }


        public static string ArchivedUsersDocument = @"
        query archivedUsers($page: Int, $limit: Int) {
          archivedUsers(page: $page, limit: $limit) {
            totalCount
            list {
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
        }
        ";
    }
}
