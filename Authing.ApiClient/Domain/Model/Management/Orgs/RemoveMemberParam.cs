using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class RemoveMemberParam
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

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("sortBy")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortByEnum? SortBy { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("includeChildrenNodes")]
        public bool? IncludeChildrenNodes { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("nodeCode")]
        public string NodeCode { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userIds")]
        public IEnumerable<string> UserIds { get; set; }

        public RemoveMemberParam(IEnumerable<string> userIds)
        {
            this.UserIds = userIds;
        }
        /// <summary>
        /// RemoveMemberParam.Request 
        /// <para>Required variables:<br/> { userIds=(string[]) }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int), sortBy=(SortByEnum), includeChildrenNodes=(bool), nodeId=(string), orgId=(string), nodeCode=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RemoveMemberDocument,
                OperationName = "removeMember",
                Variables = this
            };
        }


        public static string RemoveMemberDocument = @"
        mutation removeMember($page: Int, $limit: Int, $sortBy: SortByEnum, $includeChildrenNodes: Boolean, $nodeId: String, $orgId: String, $nodeCode: String, $userIds: [String!]!) {
          removeMember(nodeId: $nodeId, orgId: $orgId, nodeCode: $nodeCode, userIds: $userIds) {
            id
            name
            nameI18n
            description
            descriptionI18n
            order
            code
            root
            depth
            createdAt
            updatedAt
            children
            users(page: $page, limit: $limit, sortBy: $sortBy, includeChildrenNodes: $includeChildrenNodes) {
              totalCount
              list {
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
          }
        }
        ";
    }
}
