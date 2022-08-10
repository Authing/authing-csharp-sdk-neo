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
using Authing.Library.Domain.Model.V3Model;

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
            PublicKey = Options.PublicKey ?? PublicKey;
            if (UserPoolId == string.Empty)
            {
                throw new ArgumentException("UserPoolId 为空");
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
            var res = await RequestCustomDataWithOutToken<AccessTokenResponse>(GraphQLEndpoint,
                param.CreateRequest().ConvertJson(),
                contenttype: ContentType.JSON).ConfigureAwait(false);
            if (res.Errors?.Length > 0)
                throw new AuthingException(res.Errors[0].Message.Message);

            return Tuple.Create(res.Data?.Result.AccessToken, res.Data?.Result.Exp);
        }

        public async Task<GraphQLResponse<TResponse>> Request<TResponse>(GraphQLRequest body)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            return await RequestCustomDataWithToken<TResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(),
                contenttype: ContentType.JSON).ConfigureAwait(false);
        }

        public async Task<GraphQLResponse<TResponse>> RequestCustomDataWithToken<TResponse>(string url, string serializedata = "", Dictionary<string, string> headers = null!, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            headers ??= await GetHeaderWithToken(headers);
            var result = await RequestCustomData<TResponse>(url, serializedata, headers, method ?? HttpMethod.Post, contenttype).ConfigureAwait(false);
            return result;
        }

        public async Task<GraphQLResponse<TResponse>> RequestCustomDataWithToken<TResponse>(GraphQLRequest body, Dictionary<string, string> headers = null!,
            ContentType contenttype = ContentType.JSON)
        {
            var preprocessedRequest = new GraphQLHttpRequest(body);
            headers ??= await GetHeaderWithToken(headers);
            var result = await RequestCustomData<TResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(), headers, HttpMethod.Post, contenttype).ConfigureAwait(false);
            return result;
        }

        public async Task<GraphQLResponse<TResponse>> RequestCustomDataWithOutToken<TResponse>(string url,
            string serializedata = "", Dictionary<string, string>? headers = null, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            return await RequestCustomData<TResponse>(url, serializedata, headers, method, contenttype).ConfigureAwait(false);
        }

        public async Task<CommonResponse<TResponse>> RequestCustomDataWithTokenV3<TResponse>(string url,
            string serializedata = "", Dictionary<string, string> headers = null!, HttpMethod method = null!,
            ContentType contenttype = ContentType.DEFAULT)
        {
            headers ??= await GetHeaderWithToken(headers);
            var result = await RequestNoGraphQLResponse<CommonResponse<TResponse>>(url, serializedata, headers, method ?? HttpMethod.Post, contenttype).ConfigureAwait(false);
            return result;
        }

        private async Task<Dictionary<string, string>> GetHeaderWithToken(Dictionary<string, string> headers)
        { 
            headers ??= new Dictionary<string, string>();
            var token = await GetAccessToken().ConfigureAwait(false);
            headers["Authorization"] = token;
            headers["x-authing-userpool-id"] = UserPoolId;
            headers["x-authing-request-from"] = type;
            headers["x-authing-sdk-version"] = version;
            return headers;
        }
    }
}