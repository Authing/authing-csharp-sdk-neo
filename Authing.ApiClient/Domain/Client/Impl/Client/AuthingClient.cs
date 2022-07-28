using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Client.Impl.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public class AuthingClient : IAuthingClient
    {
        private readonly TimeSpan _timeOut;

        private AuthingClient(TimeSpan timeout)
        {
            _timeOut = timeout;
        }

        public static AuthingClient CreateAhtingClient(TimeSpan timeout)
        {
            return new AuthingClient(timeout);
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(string url, HttpType httpType, TRequest body,
            Dictionary<string, string> headers)
        {
            if (body is string s)
            {
                return await SendRequest<TResponse>(url, s, headers, httpType).ConfigureAwait(false);
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
                return await SendRequest<TResponse>(url, bodyStr, headers, httpType).ConfigureAwait(false);
            }
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(string url, HttpType httpType, Dictionary<string, string> body,
            Dictionary<string, string> headers)
        {
            return await SendRequest<TResponse>(url, body, headers, httpType).ConfigureAwait(false);
        }

        private async Task<TResponse> SendRequest<TResponse>(string url, string strContent,
            Dictionary<string, string> headers, HttpType httpType = HttpType.Post)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            HttpRequestMessage message = null;

            if (httpType == HttpType.Get)
            {
                message = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            }
            else if (httpType == HttpType.Delete)
            {
                message = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
            }
            else
            {
                message = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
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

                if (!headers.ContainsKey("Authorization"))
                {
                    message.Headers.Authorization = null;
                }
                if (headers.ContainsKey("Bearer"))
                {
                    message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", headers["Bearer"]);
                }
            }
            using (var httpResponseMessage =
                await HttpClientUtils.GetHttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        var res = JsonConvert.DeserializeObject<TResponse>(resString);
                        return res;
                    }
                }
                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync().ConfigureAwait(false);
                throw new Exception(content);
            }
        }

        private async Task<TResponse> SendRequest<TResponse>(string url, Dictionary<string, string> body,

           Dictionary<string, string> headers, HttpType httpType = HttpType.Post)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

            HttpRequestMessage message = null;

            if (httpType == HttpType.Get)
            {
                message = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            }
            else if (httpType == HttpType.Delete)
            {
                message = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
            }
            else if (httpType == HttpType.Patch)
            {
                SortedDictionary<string, string> sortedParam = new SortedDictionary<string, string>(body.ToDictionary(x => x.Key, x => x.Value.ToString()));
                message = new HttpRequestMessage(new HttpMethod("PATCH"), new Uri(url))

                {
                    Content = new FormUrlEncodedContent(sortedParam)
                };
            }
            else if (httpType == HttpType.Put)
            {
                SortedDictionary<string, string> sortedParam = new SortedDictionary<string, string>(body.ToDictionary(x => x.Key, x => x.Value.ToString()));
                message = new HttpRequestMessage(HttpMethod.Put, new Uri(url))

                {
                    Content = new FormUrlEncodedContent(sortedParam)
                };
            }
            else
            {
                SortedDictionary<string, string> sortedParam = new SortedDictionary<string, string>(body.ToDictionary(x => x.Key, x => x.Value.ToString()));
                message = new HttpRequestMessage(HttpMethod.Post, new Uri(url))

                {
                    Content = new FormUrlEncodedContent(sortedParam)
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
                await HttpClientUtils.GetHttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<TResponse>(resString);
                    }
                }
                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync().ConfigureAwait(false);
                throw new Exception(content);
            }
        }

        public async Task<TResponse> PutRaw<TResponse>(string url, string serializedata, Dictionary<string, string> headers)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

            HttpRequestMessage message = null;

            message = new HttpRequestMessage(HttpMethod.Put, new Uri(url))
            {
                Content = new StringContent(serializedata, Encoding.UTF8, "application/json")
                //TODO:JAVA SDK中存在 application/x-www-form-urlencoded
                //Content = new StringContent(rawjson, Encoding.UTF8, "application/x-www-form-urlencoded");
            };

            if (headers != null)
            {
                foreach (var keyValuePair in headers)
                {
                    message.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            using (var httpResponseMessage =
                await HttpClientUtils.GetHttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<TResponse>(resString);
                    }
                }
                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync().ConfigureAwait(false);
                throw new Exception(content);
            }
        }

        public async Task<TResponse> PostRaw<TResponse>(string url, string serializedata, Dictionary<string, string> headers)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

            HttpRequestMessage message = null;

            message = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
            {
                Content = new StringContent(serializedata, Encoding.UTF8, "application/json")
                //TODO:JAVA SDK中存在 application/x-www-form-urlencoded
                //Content = new StringContent(rawjson, Encoding.UTF8, "application/x-www-form-urlencoded");
            };

            if (headers != null)
            {
                foreach (var keyValuePair in headers)
                {
                    message.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            using (var httpResponseMessage =
                await HttpClientUtils.GetHttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<TResponse>(resString);
                    }
                }
                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync().ConfigureAwait(false);
                throw new Exception(content);
            }
        }

        public async Task<TResponse> RequestCustomData<TResponse>(string url, string serializedata, Dictionary<string, string> headers = null, HttpMethod method = null,
            ContentType contenttype = ContentType.DEFAULT)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

            HttpRequestMessage message = null;
            switch (method.Method)
            {
                case "GET":
                    message = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                    //message.Headers.Add("Content-Type", contenttype.ToDescription());
                    break;

                case "DELETE":
                    message = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
                    //message.Headers.Add("Content-Type", contenttype.ToDescription());
                    break;

                case "POST":
                case "PATCH":
                case "PUT":
                    message = HttpRequestMessage<TResponse>(url, serializedata, method, contenttype);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(method.Method), method, "不支持此方法");
            }

            if (headers != null)
            {
                foreach (var keyValuePair in headers)
                {
                    message.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            using (var httpResponseMessage =
                await HttpClientUtils.GetHttpClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        var resString = await reader.ReadToEndAsync().ConfigureAwait(false);

                        return JsonConvert.DeserializeObject<TResponse>(resString);
                    }
                }
                // error handling
                string content = null;
                if (contentStream != null)
                    using (var sr = new StreamReader(contentStream))
                        content = await sr.ReadToEndAsync().ConfigureAwait(false);
                throw new Exception(content);
            }
        }

        private HttpRequestMessage HttpRequestMessage<TResponse>(string url, string serializedata, HttpMethod method, ContentType contenttype)
        {
            Dictionary<string, string> data;
            SortedDictionary<string, string> sortedParam;
            HttpRequestMessage message;

            switch (contenttype)
            {
                case ContentType.DEFAULT:
                    data = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedata) ??
                           new Dictionary<string, string>();
                    sortedParam =
                        new SortedDictionary<string, string>(data?.ToDictionary(x => x.Key,
                            x => x.Value is null ? "" : x.Value.ToString()));
                    message = new HttpRequestMessage(method, new Uri(url))
                    {
                        Content = new FormUrlEncodedContent(sortedParam)
                    };
                    break;

                case ContentType.JSON:
                    message = new HttpRequestMessage(method, new Uri(url))
                    {
                        Content = new StringContent(serializedata, Encoding.UTF8, contenttype.ToDescription())
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(contenttype), contenttype, null);
            }
            return message;
        }
    }
}