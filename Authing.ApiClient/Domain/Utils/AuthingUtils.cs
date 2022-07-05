using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model;
using Authing.Library.Domain.Utils;
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

        public static IDictionary<string, object> GetPayloadByToken(string token, string pubKey, string secret, JWKS jwks = null)
        {
            List<string> tokenList = token.Split('.').ToList();

            //先判断使用那种算法来检查签名
            bool checkResult = false;
            Dictionary<string, object> headerDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Base64Url.Decode(tokenList[0])));
            if (headerDic.ContainsKey("alg"))
            {
                if (headerDic["alg"].ToString() == "HS256")
                {
                    checkResult = EncryptHelper.HMAcCheck(token, secret);
                }
                else
                {
                    string xmlPublickey = EncryptHelper.GetPublickeyFromJWKS(jwks);
                    checkResult = EncryptHelper.RSACheckWithXMLPublicKey(token, xmlPublickey);
                }
            }

            if (!checkResult)
            {
                throw new Exception("签名验证失败");
            }

            Dictionary<string, object> payloadDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Base64Url.Decode(tokenList[1])));

            return payloadDic;
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

        /// <summary>
        /// 拼接处理 query 参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CreateQueryParams<T>(T obj)
        {
            string url = "";
            string json = JsonConvert.SerializeObject(obj);

            List<KeyValuePair<string, string>> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json).ToList();

            for (int i = 0; i < dic.Count; i++)
            {
                if (dic[i].Value == null)
                {
                    continue;
                }

                string tail = dic.Count > i + 1 ? "&" : "";

                url += $"{dic[i].Key}={dic[i].Value}{tail}";
            }
            return url;
        }
    }
}
