﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public class AuthingClient : IAuthingClient
    {
        public async Task<TResponse> SendRequest<TRequest, TResponse>(string url, string method, TRequest body,
            Dictionary<string, string> headers)
        {
            if (body is string s)
            {
                return await SendRequest<TResponse>(url, s, headers, method);
            }
            else
            {
                var bodyStr = JsonConvert.SerializeObject(
                    body,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                return await SendRequest<TResponse>(url, bodyStr, headers,method);
            }
        }

        private AuthingClient()
        {
        }

        public static AuthingClient Of()
        {
            return new AuthingClient();
        }

        private async Task<TResponse> SendRequest<TResponse>(string url, string strContent,
            Dictionary<string, string> headers,string method="post")
        {
            
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

            HttpMethod httpMethod = method.ToLower() == "post" ? HttpMethod.Post : HttpMethod.Get;

            HttpRequestMessage message = null;

            if (httpMethod == HttpMethod.Get)
            {
                message = new HttpRequestMessage(httpMethod, new Uri(url))
                {
                    
                };
            }
            else
            {
                message = new HttpRequestMessage(httpMethod, new Uri(url))
                {
                    Content = new StringContent(strContent, Encoding.UTF8, "application/json")
                };
            }
             

            if (headers != null)
            {
                foreach (var keyValuePair in headers)
                {
                    message.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            using (var httpResponseMessage =
                await new HttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<TResponse>(resString);
                    }
                }

                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync();

                throw new Exception("Error");
            }
        }
    }
}