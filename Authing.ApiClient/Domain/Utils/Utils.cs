using System;
using System.Collections.Generic;
using System.Linq;
using Authing.ApiClient.Domain.Model;
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
            //TODO:确认是否需要检查签名
            var json = JwtBuilder.Create()
                .DoNotVerifySignature()
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

        public static string GenerateRandomString(int length = 30)
        {
            var rd = new Random((int)DateTime.Now.Ticks);
            var strAtt = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var resAtt = new Char[length];
            rd.Next(0, 35);
            // resAtt.ToList().ForEach(item =>
            // {
            //     item = strAtt[rd.Next(0, 35)];
            // });
            var resStr = String.Join("", resAtt.Select(p => strAtt[rd.Next(0, 35)]).ToArray());
            return resStr;
        }
    }
}
