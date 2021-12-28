using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    //#region RefreshToken
    //public class RefreshToken
    //{
    //    #region members
    //    [JsonProperty("token")]
    //    public string Token { get; set; }

    //    [JsonProperty("iat")]
    //    public int? Iat { get; set; }

    //    [JsonProperty("exp")]
    //    public int? Exp { get; set; }
    //    #endregion
    //}
    //#endregion

    //public class RefreshTokenResponse
    //{

    //    [JsonProperty("refreshToken")]
    //    public RefreshToken Result { get; set; }
    //}

    //public class RefreshTokenParam
    //{

    //    /// <summary>
    //    /// Optional
    //    /// </summary>
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    public RefreshTokenParam()
    //    {

    //    }
    //    /// <summary>
    //    /// RefreshTokenParam.Request 
    //    /// <para>Required variables:<br/> {  }</para>
    //    /// <para>Optional variables:<br/> { id=(string) }</para>
    //    /// </summary>
    //    public GraphQLRequest CreateRequest()
    //    {
    //        return new GraphQLRequest
    //        {
    //            Query = RefreshTokenDocument,
    //            OperationName = "refreshToken",
    //            Variables = this
    //        };
    //    }


    //    public static string RefreshTokenDocument = @"
    //    mutation refreshToken($id: String) {
    //      refreshToken(id: $id) {
    //        token
    //        iat
    //        exp
    //      }
    //    }
    //    ";
    //}
}
