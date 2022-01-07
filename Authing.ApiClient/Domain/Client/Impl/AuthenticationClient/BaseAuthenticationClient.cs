using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Util;

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
            UserPoolId = Options.UserPoolId ?? UserPoolId;
            if (AppId == string.Empty)
            {
                throw new Exception("参数错误");
            }
        }

        public async Task<string> GetAccessToken()
        {
            long now = DateTimeOffset.Now.Second;

            if (accessTokenExpiredAt.HasValue && accessTokenExpiredAt > now + 3600)
            {
                return AccessToken;
            }

            var tuple = await GetAccessTokenFromServer().ConfigureAwait(false);
            AccessToken = tuple.Item1;
            accessTokenExpiredAt = tuple.Item2;
            return AccessToken;
        }

        private async Task<Tuple<string, int?>> GetAccessTokenFromServer()
        {
            var param = new Model.AccessTokenParam(UserPoolId, Secret);
            //  如果不加 WithAccessToken 會死循環
            var res = await PostWithoutToken<GraphQLResponse<Model.AccessTokenResponse>>(param.CreateRequest()).ConfigureAwait(false);


            return Tuple.Create(res.Data.Result.AccessToken, res.Data.Result.Exp);
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body, string accessToken = null)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Request<TResponse>(body, headers).ConfigureAwait(false);
        }

        protected async Task<GraphQLResponse<TResponse>> Request<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Request<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Request<TResponse>(body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, string> body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Post<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, object> body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var item in body)
            {
                if (item.Value is string)
                {
                    dic.Add(item.Key, item.Value.ToString());
                    continue;
                }
                if (item.Value is int)
                {
                    dic.Add(item.Key, item.Value.ToString());
                    continue;
                }
                dic.Add(item.Key, Newtonsoft.Json.JsonConvert.SerializeObject(item.Value));

            }
            return await Post<TResponse>(api, dic, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Post<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Get<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Get<GraphQLRequest, TResponse>(api, body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> Delete<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(true);
            return await Delete<GraphQLRequest, TResponse>(api, body, headers).ConfigureAwait(false);
        }

        protected async Task<TResponse> PostWithoutToken<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers = GetAuthHeaders(false);
            return await Post<TResponse>(body, headers).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> PostRaw<TResponse>(string api, string rawjson, string accessToken = null)
        {
            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(accessToken))
            {
                headers["Authorization"] = "bearer " + accessToken;
            }
            else
            {
                var token = await GetAccessToken().ConfigureAwait(false);
                headers["Authorization"] = "bearer " + token;
            }

            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await PostRaw<TResponse>(api, rawjson, headers).ConfigureAwait(false);
        }

        protected async Task<GraphQLResponse<TResponse>> RequestCustomDataWithToken<TResponse>(string url,
            string serializedata = "", Dictionary<string, string>? headers = null, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            var amphitheaters = GetAuthHeaders(true);
            if (headers != null)
                foreach (var pair in headers)
                {
                    if (amphitheaters.ContainsKey(pair.Key))
                        amphitheaters[pair.Key] = pair.Value;
                }
            return await RequestCustomData<TResponse>(url, serializedata, amphitheaters, method, contenttype).ConfigureAwait(false);
        }

        public Dictionary<string, string> GetAuthHeaders(bool withToken = false)
        {
            var dic = new Dictionary<string, string>
            {

                { "x-authing-request-from",type},
                { "x-authing-sdk-version",version}
            };

            if (!string.IsNullOrEmpty(AccessToken) || withToken)
            {
                dic.Add("Authorization", "Bearer " + AccessToken);
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

        public Dictionary<string, string> GetTestHeader()
        {
            var dic = new Dictionary<string, string>
            {

                { "request-from",type},
                { "sdk-version",version},
                { "userpool-id",UserPoolId},
                { "app-id",AppId}

            };
            return dic;
        }

    }
}
