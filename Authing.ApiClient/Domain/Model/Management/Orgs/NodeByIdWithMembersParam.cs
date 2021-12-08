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
    public class NodeByIdWithMembersParam
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
        /// Required
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public NodeByIdWithMembersParam(string id)
        {
            this.Id = id;
        }
        /// <summary>
        /// NodeByIdWithMembersParam.Request 
        /// <para>Required variables:<br/> { id=(string) }</para>
        /// <para>Optional variables:<br/> { page=(int), limit=(int), sortBy=(SortByEnum), includeChildrenNodes=(bool) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = NodeByIdWithMembersDocument,
                OperationName = "nodeByIdWithMembers",
                Variables = this
            };
        }


        public static string NodeByIdWithMembersDocument = @"
        query nodeByIdWithMembers($page: Int, $limit: Int, $sortBy: SortByEnum, $includeChildrenNodes: Boolean, $id: String!) {
          nodeById(id: $id) {
            id
            orgId
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
                externalId
              }
            }
          }
        }
        ";
    }
}
