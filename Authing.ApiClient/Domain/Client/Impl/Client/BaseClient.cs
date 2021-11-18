using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client
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
        public string Host { get; set; } = "https://core.authing.cn";

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

        protected readonly string type = "SDK";
        protected readonly string version = "c-sharp:4.2.4.7";

        protected BaseClient()
        {
            this.client = AuthingClient.Of();
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest body, Dictionary<string, string> headers)
        {
            return await this.client.SendRequest<TRequest, TResponse>("", "Post", body, headers);
        }

        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body, Dictionary<string, string> headers)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var bodyString = preprocessedRequest.ToHttpRequestBody();
            return await client.SendRequest<string, TResponse>(GraphQLEndpoint, "Post", bodyString,
                headers ?? new Dictionary<string, string>());
        }

        public async Task<TResponse> Get<TRequest, TResponse>(TRequest body, Dictionary<string, string> headers)
        {
            return await client.SendRequest<TRequest, TResponse>("", "Get", body, headers);
        }


        private static void CheckResult<T>(GraphQLResponse<T> result)
        {
            if (result.Errors != null && result.Errors.Length > 0)
            {
                var error = result.Errors[0].Message;
                throw new AuthingException(error.Message, error.Code);
            }
        }
    }
}