using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class AccessTokenParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        public AccessTokenParam(string userPoolId, string secret)
        {
            this.UserPoolId = userPoolId;
            this.Secret = secret;
        }
        /// <summary>
        /// AccessTokenParam.Request 
        /// <para>Required variables:<br/> { userPoolId=(string), secret=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = AccessTokenDocument,
                OperationName = "accessToken",
                Variables = this
            };
        }


        public static string AccessTokenDocument = @"
        query accessToken($userPoolId: String!, $secret: String!) {
          accessToken(userPoolId: $userPoolId, secret: $secret) {
            accessToken
            exp
            iat
          }
        }
        ";
    }
}