using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ResetPasswordParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }

        public ResetPasswordParam(string code, string newPassword)
        {
            this.Code = code;
            this.NewPassword = newPassword;
        }
        /// <summary>
        /// ResetPasswordParam.Request 
        /// <para>Required variables:<br/> { code=(string), newPassword=(string) }</para>
        /// <para>Optional variables:<br/> { phone=(string), email=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = ResetPasswordDocument,
                OperationName = "resetPassword",
                Variables = this
            };
        }


        public static string ResetPasswordDocument = @"
        mutation resetPassword($phone: String, $email: String, $code: String!, $newPassword: String!) {
          resetPassword(phone: $phone, email: $email, code: $code, newPassword: $newPassword) {
            message
            code
          }
        }
        ";
    }
}
