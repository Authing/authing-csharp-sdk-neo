using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public abstract class BaseClient
    {
        private int? accessTokenExpiredAt = 0;
        public IAuthingClient client { get; private set; }
        public string AccessToken { get; set; }

        /// <summary>
        /// 接口超时时间，默认为 10 秒
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

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
            get { return Host + "/graphql/v2"; }
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
            this.client = AuthingClient.CreateAhtingClient();
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest body, Dictionary<string, string> headers)
        {
            return await this.client.SendRequest<TRequest, TResponse>("", "Post", body, headers);
        }

        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            var result = await client.SendRequest<string, TResponse>(GraphQLEndpoint, "Post", bodyString,
                headers ?? new Dictionary<string, string>());
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            var result = await client.SendRequest<string, GraphQLResponse<TResponse>>(GraphQLEndpoint, "Post", bodyString,
                headers ?? new Dictionary<string, string>());
            CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(string api,GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            var result = await client.SendRequest<string, GraphQLResponse<TResponse>>(GraphQLEndpoint, "Post", bodyString,
                headers ?? new Dictionary<string, string>());
            CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            var result= await client.SendRequest<string, GraphQLResponse<TResponse>>(Host +$"/{api}", "Post", bodyString,
                headers ?? new Dictionary<string, string>());
            CheckResult(result);
            return result;
        }

        public async Task<GraphQLResponse<TResponse>> Get<TRequest, TResponse>(TRequest body, Dictionary<string, string> headers)
        {
            var result= await client.SendRequest<TRequest, GraphQLResponse<TResponse>>("", "Get", body, headers);
            CheckResult(result);
            return result;
        }

        protected async Task<GraphQLResponse<TResponse>> Get<TRequest, TResponse>(string api,TRequest body, Dictionary<string, string> headers)
        {
            var result= await client.SendRequest<TRequest, GraphQLResponse<TResponse>>(Host + $"/{api}", "Get", body, headers);
            CheckResult(result);
            return result;
        }


        private static void CheckResult<T>(GraphQLResponse<T> result)
        {
            if(result.Data == null && result.Errors.Length == 0)
            {
                var error = result.Errors[0].Message;
                throw new AuthingException("Server return data Null !");
            }
            if (result.Errors != null && result.Errors.Length > 0)
            {
                var error = result.Errors[0].Message;
                throw new AuthingException(error.Message, error.Code);
            }
        }
    }
}