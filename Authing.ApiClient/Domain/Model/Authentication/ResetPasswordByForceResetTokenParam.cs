using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ResetPasswordByForceResetTokenParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }

        public ResetPasswordByForceResetTokenParam(string token, string oldPassword, string newPassword)
        {
            this.Token = token;
            this.OldPassword = oldPassword;
            this.NewPassword = newPassword;
        }
        /// <summary>
        /// ResetPasswordByForceResetTokenParam.Request 
        /// <para>Required variables:<br/> { token=(string), oldPassword=(string), newPassword=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ResetPasswordByForceResetTokenDocument,
                OperationName = "resetPasswordByForceResetToken",
                Variables = this
            };
        }


        public static string ResetPasswordByForceResetTokenDocument = @"
        mutation resetPasswordByForceResetToken($token: String!, $oldPassword: String!, $newPassword: String!) {
          resetPasswordByForceResetToken(token: $token, oldPassword: $oldPassword, newPassword: $newPassword) {
            message
            code
          }
        }
        ";
    }
}
