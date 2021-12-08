using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public abstract class BaseManagementClient : BaseClient
    {
        private int? accessTokenExpiredAt = 0;
        public string UserPoolId { get; private set; }
        public string Secret { get; set; }
        public InitAuthenticationClientOptions Options { get; protected set; } = new();

        protected BaseManagementClient(string userPoolId, string secret)
        {
            UserPoolId = userPoolId ?? throw new ArgumentNullException(nameof(userPoolId));
            Secret = secret;
        }

        protected BaseManagementClient(Action<InitAuthenticationClientOptions> init)
        {
            if (init == null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            init(Options);
            UserPoolId = Options.UserPoolId;
            Secret = Options.Secret;
            Host = Options.Host ?? Host;
            if (UserPoolId == string.Empty)
            {
                throw new Exception("参数错误");
            }
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
            var param = new Model.AccessTokenParam(UserPoolId, Secret);
            //  如果不加 WithAccessToken 會死循環
            var res = await PostWithoutToken<GraphQLResponse<Model.AccessTokenResponse>>(param.CreateRequest());


            return Tuple.Create(res.Data.Result.AccessToken, res.Data.Result.Exp);
        }


        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Request<TResponse>(body, headers);
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Request<TResponse>(api, body, headers);
        }


        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Request<TResponse>(body, headers);
        }



        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api,Dictionary<string,string> body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Post<TResponse>(api,body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api,GraphQLRequest body)

        {

            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Post<TResponse>(api,body, headers);
        }


        public async Task<GraphQLResponse<TResponse>> Get<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Get<GraphQLRequest, TResponse>(api, body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Delete<TResponse>(string api, GraphQLRequest body)

        {

            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken();
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Delete<GraphQLRequest, TResponse>(api, body, headers);
        }

        protected async Task<TResponse> PostWithoutToken<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Post<TResponse>(body, headers);
        }





        public object GetAuthHeaders()
        {
            return new
            {
                x_authing_userpool_id = UserPoolId,
                x_authing_request_from = type,
                x_authing_sdk_version = version,
            };
        }
    }
}