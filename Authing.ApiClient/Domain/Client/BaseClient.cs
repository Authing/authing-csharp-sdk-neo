using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client
{
    public abstract class BaseClient
    {
        private int? accessTokenExpiredAt = 0;
        public IAuthingClient client { get; private set; }
        public string UserPoolId { get; private set; }
        public string Secret { get; set; }
        public string AppId { get; private set; }
        public InitAuthenticationClientOptions Options { get; protected set; } = new();
        public string AccessToken { get; set; }

        /// <summary>
        /// 接口超时时间，默认为 10 秒
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Authing 接口 URL，默认为 https://core.authing.cn
        /// </summary>
        public string Host { get; set; } = "https://core.authing.cn";

        private string GraphQLEndpoint
        {
            get { return Host + "/graphql/v2"; }
        }

        public string MFAToken { get; set; }

        /// <summary>
        /// 加密密码使用的公钥
        /// </summary>
        public string PublicKey { get; set; } = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4xKeUgQ+Aoz7TLfAfs9+paePb
5KIofVthEopwrXFkp8OCeocaTHt9ICjTT2QeJh6cZaDaArfZ873GPUn00eOIZ7Ae
+TiA2BKHbCvloW3w5Lnqm70iSsUi5Fmu9/2+68GZRH9L7Mlh8cFksCicW2Y2W2uM
GKl64GDcIq3au+aqJQIDAQAB
-----END PUBLIC KEY-----";

        private readonly string type = "SDK";
        private readonly string version = "c-sharp:4.2.4.7";

        protected BaseClient(string userPoolId, string secret)
        {
            UserPoolId = userPoolId ?? throw new ArgumentNullException(nameof(userPoolId));
            Secret = secret;
            this.client = client;
            this.client = AuthingClient.Of();
        }

        protected BaseClient(Action<InitAuthenticationClientOptions> init)
        {
            if (init == null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            init(Options);
            UserPoolId = Options.UserPoolId;
            AppId = Options.AppId;
            Secret = Options.Secret;
            Host = Options.Host ?? Host;
            if (UserPoolId == string.Empty && AppId == string.Empty)
            {
                throw new Exception("参数错误");
            }

            this.client = AuthingClient.Of();
        }

        protected async Task<string> GetAccessToken()
        {
            long now = DateTimeOffset.Now.Second;

            if (accessTokenExpiredAt.HasValue && accessTokenExpiredAt > now + 3600)
            {
                return AccessToken;
            }

            var tuple = await GetAccessTokenFromServer();
            AccessToken = tuple.Item1;
            accessTokenExpiredAt = tuple.Item2;
            return AccessToken;
        }


        private async Task<Tuple<string, int?>> GetAccessTokenFromServer()
        {
            var param = new AccessTokenParam(UserPoolId, Secret);
            //  如果不加 WithAccessToken 會死循環
            var res = await Post<GraphQLResponse<AccessTokenResponse>>(param.CreateRequest(),
                new RequestOption {WithAccessToken = false});


            return Tuple.Create(res.Data.Result.AccessToken, res.Data.Result.Exp);
        }

        public async Task<TResponse> Post<TRequest, TResponse>(TRequest body, Dictionary<string, string> headers)
        {
            return await this.client.SendRequest<TRequest, TResponse>("", "Post", body, headers);
        }


        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body)
        {
            var defaultOption = new RequestOption();
            return await Post<TResponse>(body, defaultOption);
        }

        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body, RequestOption option)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            var headers = new Dictionary<string, string>();
            if (option.WithAccessToken)
            {
                var token = await GetAccessToken();
                headers["Authorization"] = token;
            }

            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-app-id"] = AppId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;

            var bodyString = preprocessedRequest.ToHttpRequestBody();

            return await client.SendRequest<string, TResponse>(GraphQLEndpoint, "Post", bodyString, headers);
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

        public object GetAuthHeaders()
        {
            return new
            {
                x_authing_userpool_id =
                    UserPoolId,
                x_authing_app_id = AppId,
                x_authing_request_from = type,
                x_authing_sdk_version = version,
            };
        }
    }
}