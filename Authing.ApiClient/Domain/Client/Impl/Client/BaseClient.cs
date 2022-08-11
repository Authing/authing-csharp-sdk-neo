using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Infrastructure.GraphQL;

using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public abstract class BaseClient
    {
        private int? accessTokenExpiredAt = 0;
        protected IAuthingClient client { get; private set; }

        public string MFAToken { get; set; }
        public string AccessToken { get; set; }

        /// <summary>
        /// 接口超时时间，默认为 10 秒
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Authing 接口 URL，默认为 https://core.authing.cn
        /// </summary>
#if TEST_ENV
        public string Host { get; set; } = "https://core.test.authing-inc.co";
#elif DEV_ENV
        public string Host { get; set; } = "https://core.dev.authing-inc.co";
#else
        public string Host { get; set; } = "https://core.authing.cn";
#endif

        protected string GraphQLEndpoint
        {
            get { return "graphql/v2"; }
        }

        /// <summary>
        /// 加密密码使用的公钥
        /// </summary>
        public string PublicKey { get; set; } = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4xKeUgQ+Aoz7TLfAfs9+paePb
5KIofVthEopwrXFkp8OCeocaTHt9ICjTT2QeJh6cZaDaArfZ873GPUn00eOIZ7Ae
+TiA2BKHbCvloW3w5Lnqm70iSsUi5Fmu9/2+68GZRH9L7Mlh8cFksCicW2Y2W2uM
GKl64GDcIq3au+aqJQIDAQAB
-----END PUBLIC KEY-----";

        public readonly string type = "SDK";
        public readonly string version = "c-sharp:4.2.4.7";

        protected BaseClient()
        {
            this.client = AuthingClient.CreateAhtingClient(Timeout);

            var currentversion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            version = $"c-sharp:{currentversion.Major}:{currentversion.Minor}:{currentversion.Build}:{currentversion.Revision}";
        }

        protected async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, string> body, Dictionary<string, string> headers)
        {
            var result = await client.SendRequest<string, GraphQLResponse<TResponse>>(Host + $"/{api}", HttpType.Post, body,
                headers ?? new Dictionary<string, string>()).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            var result = await client.SendRequest<string, GraphQLResponse<TResponse>>($"{Host}/{GraphQLEndpoint}", HttpType.Post, bodyString,
                headers ?? new Dictionary<string, string>()).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Get<TRequest, TResponse>(string api, TRequest body, Dictionary<string, string> headers)
        {
            var result = await client.SendRequest<TRequest, GraphQLResponse<TResponse>>(Host + $"/{api}", HttpType.Get, body, headers).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Delete<TRequest, TResponse>(string api, TRequest body, Dictionary<string, string> headers)
        {
            var result = await client.SendRequest<TRequest, GraphQLResponse<TResponse>>(Host + $"/{api}", HttpType.Delete, body, headers).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> PostRaw<TResponse>(string api, string rawjson,
            Dictionary<string, string> headers = null)
        {
            var result = await client.PostRaw<GraphQLResponse<TResponse>>(Host + $"/{api}", rawjson, headers ?? new Dictionary<string, string>()).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> RequestCustomData<TResponse>(string url, string serializedata = "", Dictionary<string, string> headers = null!, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            var result = await client.RequestCustomData<GraphQLResponse<TResponse>>(Host + $"/{url}", serializedata, headers, method ?? HttpMethod.Post, contenttype).ConfigureAwait(false);
            //CheckResult(result);
            return result;
        }

        protected async Task<TResponse> RequestNoGraphQLResponse<TResponse>(string url, string serializedata = "", Dictionary<string, string> headers = null!, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            var result = await client.RequestCustomData<TResponse>(Host + $"/{url}", serializedata, headers, method ?? HttpMethod.Post, contenttype).ConfigureAwait(false);
            return result;
        }
    }
}