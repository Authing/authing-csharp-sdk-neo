using System;
using System.Collections.Generic;
using Authing.ApiClient.Core.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Types;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;

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

        public static IEnumerable<ResUdv> ConvertUdv(IEnumerable<UserDefinedData> udvList)
        {
            var resUdvList = new List<ResUdv>();
            foreach (var udv in udvList)
            {
                object value = udv.DataType switch
                {
                    UdfDataType.STRING => udv.Value,
                    UdfDataType.NUMBER => int.Parse(udv.Value),
                    UdfDataType.DATETIME => new DateTime(int.Parse(udv.Value), DateTimeKind.Utc),
                    UdfDataType.BOOLEAN => JsonConvert.DeserializeObject<bool>(udv.Value),
                    UdfDataType.OBJECT
                        => JsonConvert.DeserializeObject<object>(udv.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };

                resUdvList.Add(new ResUdv()
                {
                    DataType = udv.DataType,
                    Key = udv.Key,
                    Value = value,
                    Label = udv.Label,
                });
            }

            return resUdvList;
        }

        public static List<KeyValuePair<string, object>> ConverUdvToKeyValuePair(IEnumerable<UserDefinedData> udvList)
        {
            var resUdvList = new List<KeyValuePair<string, object>>();
            foreach (var udv in udvList)
            {
                object value = udv.DataType switch
                {
                    UdfDataType.STRING => udv.Value,
                    UdfDataType.NUMBER => int.Parse(udv.Value),
                    UdfDataType.DATETIME => new DateTime(int.Parse(udv.Value), DateTimeKind.Utc),
                    UdfDataType.BOOLEAN => JsonConvert.DeserializeObject<bool>(udv.Value),
                    UdfDataType.OBJECT
                        => JsonConvert.DeserializeObject<object>(udv.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };

                resUdvList.Add(new KeyValuePair<string, object>(udv.Key, value));
            }

            return resUdvList;
        }
    }
}
