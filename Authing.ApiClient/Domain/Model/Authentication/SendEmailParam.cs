using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class SendEmailParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("scene")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EmailScene Scene { get; set; }

        public SendEmailParam(string email, EmailScene scene)
        {
            this.Email = email;
            this.Scene = scene;
        }
        /// <summary>
        /// SendEmailParam.Request 
        /// <para>Required variables:<br/> { email=(string), scene=(EmailScene) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SendEmailDocument,
                OperationName = "sendEmail",
                Variables = this
            };
        }


        public static string SendEmailDocument = @"
        mutation sendEmail($email: String!, $scene: EmailScene!) {
          sendEmail(email: $email, scene: $scene) {
            message
            code
          }
        }
        ";
    }
}
