using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.GraphQLParam
{
    public class CheckLoginStatusParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        public CheckLoginStatusParam()
        {

        }
        /// <summary>
        /// CheckLoginStatusParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { token=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CheckLoginStatusDocument,
                OperationName = "checkLoginStatus",
                Variables = this
            };
        }


        public static string CheckLoginStatusDocument = @"
        query checkLoginStatus($token: String) {
          checkLoginStatus(token: $token) {
            code
            message
            status
            exp
            iat
            data {
              id
              userPoolId
              arn
            }
          }
        }
        ";
    }
}