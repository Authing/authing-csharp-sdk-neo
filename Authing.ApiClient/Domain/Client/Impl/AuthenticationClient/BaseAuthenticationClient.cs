using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.GraphQLParam;
using Authing.ApiClient.Extensions;

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
            Secret = Options.Secret;
            PublicKey = Options.PublicKey ?? PublicKey;
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
            var param = new AccessTokenParam(UserPoolId, Secret);
            //  如果不加 WithAccessToken 會死循環
            var res = await RequestCustomDataWithOutToken<AccessTokenResponse>(GraphQLEndpoint, param.CreateRequest().ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);

            return Tuple.Create(res.Data.Result.AccessToken, res.Data.Result.Exp);
        }

        public async Task<GraphQLResponse<TResponse>> RequestCustomDataWithOutToken<TResponse>(GraphQLRequest body)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            return await RequestCustomDataWithOutToken<TResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(),
                contenttype: ContentType.JSON).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> RequestCustomDataWithToken<TResponse>(GraphQLRequest body)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            return await RequestCustomDataWithToken<TResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(),
                contenttype: ContentType.JSON).ConfigureAwait(false);
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

        protected async Task<GraphQLResponse<TResponse>> RequestCustomDataWithOutToken<TResponse>(string url,
            string serializedata = "", Dictionary<string, string>? headers = null, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            var amphitheaters = GetAuthHeaders(false);
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
    }
}