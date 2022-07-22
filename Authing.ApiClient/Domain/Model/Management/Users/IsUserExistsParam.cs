using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class IsUserExistsParam
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

        public IsUserExistsParam()
        {
        }

        /// <summary>
        /// IsUserExistsParam.Request
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { email=(string), phone=(string), username=(string), externalId=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = IsUserExistsDocument,
                OperationName = "isUserExists",
                Variables = this
            };
        }

        public static string IsUserExistsDocument = @"
        query isUserExists($email: String, $phone: String, $username: String, $externalId: String) {
          isUserExists(email: $email, phone: $phone, username: $username, externalId: $externalId)
        }
        ";
    }
}