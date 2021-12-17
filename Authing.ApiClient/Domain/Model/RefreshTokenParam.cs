using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model
{
    public class RefreshTokenParam
    {

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        public RefreshTokenParam()
        {

        }
        /// <summary>
        /// RefreshTokenParam.Request 
        /// <para>Required variables:<br/> {  }</para>
        /// <para>Optional variables:<br/> { id=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RefreshTokenDocument,
                OperationName = "refreshToken",
                Variables = this
            };
        }


        public static string RefreshTokenDocument = @"
        mutation refreshToken($id: String) {
          refreshToken(id: $id) {
            token
            iat
            exp
          }
        }
        ";
    }
}
