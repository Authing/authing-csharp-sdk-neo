using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public class BaseAuthenticationClient : BaseClient
    {
        private int? accessTokenExpiredAt = 0;
        public string UserPoolId { get; private set; }

        public string Secret { get; set; }

        /// <summary>
        /// AppID，注意用户池 ID 与 AppID，必填其一
        /// </summary>
        protected string AppId { get; private set; }

        public InitAuthenticationClientOptions Options { get; protected set; } = new();

        public BaseAuthenticationClient(string appId)
        {
            this.AppId = appId;
        }

        public BaseAuthenticationClient(Action<InitAuthenticationClientOptions> init)
        {
            if (init == null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            init(Options);
            Host = Options.Host ?? Host;
            AppId = Options.AppId ?? AppId;
            if (AppId == string.Empty)
            {
                throw new Exception("参数错误");
            }
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body,string accessToken=null)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Request<TResponse>(body, headers);
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Request<TResponse>(api, body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Request<TResponse>(body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, string> body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Post<TResponse>(api, body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Post<TResponse>(api, body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Get<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(true);
            return await Get<GraphQLRequest, TResponse>(api, body, headers);
        }

        public async Task<GraphQLResponse<TResponse>> Delete<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            //var token = await GetAccessToken();
            //headers["Authorization"] = token;
            //headers["x-authing-userpool-id"] = UserPoolId;
            //headers["x-authing-request-from"] = type;
            //headers["x-authing-sdk-version"] = version;
            headers = await GetAuthHeaders(true);
            return await Delete<GraphQLRequest, TResponse>(api, body, headers);
        }

        protected async Task<TResponse> PostWithoutToken<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = await GetAuthHeaders(false);
            return await Post<TResponse>(body, headers);
        }

        public async Task< Dictionary<string, string>> GetAuthHeaders(bool withToken=false)
        {
            var dic= new Dictionary<string, string>
            {
              
                { "x-authing-request-from",type},
                { "x-authing-sdk-version",version}
            };

            //if (withToken)
            //{
            //    var token = await GetAccessToken();
            //    dic.Add("Authorization", token);
            //}

            if (!string.IsNullOrEmpty(AccessToken))
            {
                dic.Add("Bearer", AccessToken);
            }

            if (!string.IsNullOrEmpty(UserPoolId))
            {
                dic.Add("x-authing-userpool-id", UserPoolId);
            }
            if (!string.IsNullOrEmpty(AppId))
            {
                dic.Add("x-authing-app-id", AppId);
            }

            return dic;
        }
    }
}
