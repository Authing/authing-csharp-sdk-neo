using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.GraphQLParam;
using Authing.ApiClient.Extensions;
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
                throw new ArgumentException("UserPoolId 为空");
            }
        }

        public async Task<string> GetAccessToken()
        {
            //TODO:判断 Token 过期时间问题有待商榷
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
            var param = new AccessTokenParam(UserPoolId, Secret);
            //  如果不加 WithAccessToken 會死循環
            var res = await PostWithoutToken<GraphQLResponse<Model.AccessTokenResponse>>(param.CreateRequest()).ConfigureAwait(false);

            if (res.Errors?.Length > 0)
                throw new AuthingException(res.Errors[0].Message.Message);

            return Tuple.Create(res.Data?.Result.AccessToken, res.Data?.Result.Exp);
        }

        public async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Request<TResponse>(body, headers).ConfigureAwait(false);
        }

        public async Task<TResponse> PostWithoutToken<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Post<TResponse>(body, headers).ConfigureAwait(false);
        }

        //public async Task<GraphQLResponse<TResponse>> Request<TResponse>(string api, GraphQLRequest body)
        //{
        //    var headers = new Dictionary<string, string>();
        //    var token = await GetAccessToken();
        //    headers["Authorization"] = token;
        //    headers["x-authing-userpool-id"] = UserPoolId;
        //    headers["x-authing-request-from"] = type;
        //    headers["x-authing-sdk-version"] = version;
        //    return await Request<TResponse>(api, body, headers);
        //}

        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Request<TResponse>(body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, string> body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Post<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, Dictionary<string, object> body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;


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

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Get<TResponse>(string api, GraphQLRequest body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Get<GraphQLRequest, TResponse>(api, body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Delete<TResponse>(string api, GraphQLRequest body)
        {

            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Delete<GraphQLRequest, TResponse>(api, body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Patch<TResponse>(string api, Dictionary<string, string> body)
        {

            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Patch<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> Put<TResponse>(string api, Dictionary<string, string> body)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await Put<TResponse>(api, body, headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> PostRaw<TResponse>(string api, Dictionary<string,object> dic)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await PostRaw<TResponse>(api, dic.ConvertJson(), headers).ConfigureAwait(false);
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<GraphQLResponse<TResponse>> PutRaw<TResponse>(string api, Dictionary<string, object> dic)
        {
            var headers = new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return await PostRaw<TResponse>(api, dic.ConvertJson(), headers).ConfigureAwait(false);
        }

        public new async Task<GraphQLResponse<TResponse>> RequestCustomDataWithToken<TResponse>(string url, string serializedata = "", Dictionary<string, string> headers = null!, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            headers ??= new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            var result = await client.RequestCustomData<GraphQLResponse<TResponse>>(Host + $"/{url}", serializedata, headers, method ?? HttpMethod.Post, contenttype).ConfigureAwait(false);
            return result;
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