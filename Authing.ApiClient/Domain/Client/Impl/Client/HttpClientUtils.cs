using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Authing.Library.Domain.Client.Impl.Client
{
    internal sealed class HttpClientUtils
    {
        private static HttpClient _httpClient;
        private static readonly object _lock = new object();
        private static readonly HttpClientUtils _httpClientUtils = new HttpClientUtils();

        static HttpClientUtils()
        {
            _httpClient = GetHttpClient();
        }

        public static HttpClientUtils GetHttpClientUtils()
        {
            return _httpClientUtils;
        }

        private HttpClientUtils()
        {
        }

        public static HttpClient GetHttpClient()
        {
            lock (_lock)
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                    //_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.Timeout = TimeSpan.FromSeconds(60);
                }
            }
            return _httpClient;
        }
    }
}