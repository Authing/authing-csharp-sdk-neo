using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ResetPasswordByFirstLoginTokenParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        public ResetPasswordByFirstLoginTokenParam(string token, string password)
        {
            this.Token = token;
            this.Password = password;
        }
        /// <summary>
        /// ResetPasswordByFirstLoginTokenParam.Request 
        /// <para>Required variables:<br/> { token=(string), password=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ResetPasswordByFirstLoginTokenDocument,
                OperationName = "resetPasswordByFirstLoginToken",
                Variables = this
            };
        }


        public static string ResetPasswordByFirstLoginTokenDocument = @"
        mutation resetPasswordByFirstLoginToken($token: String!, $password: String!) {
          resetPasswordByFirstLoginToken(token: $token, password: $password) {
            message
            code
          }
        }
        ";
    }
}
