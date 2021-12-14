using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class FindUserOption : ExistsOption
    {
        public string? ExternalId { get; set; }
    }

    public class FindUserResponse
    {

        [JsonProperty("findUser")]
        public User Result { get; set; }
    }

    #region FindUserByIdentityInput
    public class FindUserByIdentityInput
    {
        #region members
        [JsonProperty("provider")]
        [JsonRequired]
        public string Provider { get; set; }

        [JsonProperty("userIdInIdp")]
        [JsonRequired]
        public string UserIdInIdp { get; set; }
        #endregion


        /// <summary>
        /// <param name="provider">provider</param>
        /// <param name="userIdInIdp">userIdInIdp</param>
        /// </summary>

        public FindUserByIdentityInput(string provider, string userIdInIdp)
        {
            this.Provider = provider;
            this.UserIdInIdp = userIdInIdp;
        }

        #region methods
        public dynamic GetInputObject()
        {
            IDictionary<string, object> d = new System.Dynamic.ExpandoObject();

            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(this, null);
                var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
                if (requiredProp || value != defaultValue)
                {
                    d[propertyInfo.Name] = value;
                }
            }
            return d;
        }
        #endregion
    }
    #endregion

    public class FindUserParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("identity")]
        public FindUserByIdentityInput Identity { get; set; }

        public FindUserParam()
        {

        }
        /// <summary>
        /// FindUserParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { email=(string), phone=(string), username=(string), externalId=(string), identity=(FindUserByIdentityInput) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = FindUserDocument,
                OperationName = "findUser",
                Variables = this
            };
        }


        public static string FindUserDocument = @"
        query findUser($email: String, $phone: String, $username: String, $externalId: String, $identity: FindUserByIdentityInput) {
          findUser(email: $email, phone: $phone, username: $username, externalId: $externalId, identity: $identity) {
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
