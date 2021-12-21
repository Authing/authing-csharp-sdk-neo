using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class CheckPasswordStrengthParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        public CheckPasswordStrengthParam(string password)
        {
            this.Password = password;
        }
        /// <summary>
        /// CheckPasswordStrengthParam.Request 
        /// <para>Required variables:<br/> { password=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = CheckPasswordStrengthDocument,
                OperationName = "checkPasswordStrength",
                Variables = this
            };
        }


        public static string CheckPasswordStrengthDocument = @"
        query checkPasswordStrength($password: String!) {
          checkPasswordStrength(password: $password) {
            valid
            message
          }
        }
        ";
    }
}
