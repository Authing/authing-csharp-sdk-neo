using System;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class SendFirstLoginVerifyEmailResponse
    {

        [JsonProperty("sendFirstLoginVerifyEmail")]
        public CommonMessage Result { get; set; }
    }

    public class SendFirstLoginVerifyEmailParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("appId")]
        public string AppId { get; set; }

        public SendFirstLoginVerifyEmailParam(string userId, string appId)
        {
            this.UserId = userId;
            this.AppId = appId;
        }
        /// <summary>
        /// SendFirstLoginVerifyEmailParam.Request 
        /// <para>Required variables:<br/> { userId=(string), appId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SendFirstLoginVerifyEmailDocument,
                OperationName = "sendFirstLoginVerifyEmail",
                Variables = this
            };
        }


        public static string SendFirstLoginVerifyEmailDocument = @"
        mutation sendFirstLoginVerifyEmail($userId: String!, $appId: String!) {
          sendFirstLoginVerifyEmail(userId: $userId, appId: $appId) {
            message
            code
          }
        }
        ";
    }
}
