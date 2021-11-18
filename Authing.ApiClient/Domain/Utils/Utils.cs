using System;
using System.Collections.Generic;
using Authing.ApiClient.Types;
using JWT.Algorithms;
using JWT.Builder;

namespace Authing.ApiClient.Domain.Utils
{
    public static class AuthingUtils
    {
        public static ListParams ListParams { get; set; } = new ListParams()
        {
            Limit = 10,
            Page = 1,
        };

        public static IDictionary<string, object> GetPayloadByToken(string token)
        {
            var json = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(token);                    
            Console.WriteLine(json);
            return json;
        }
    }
}
