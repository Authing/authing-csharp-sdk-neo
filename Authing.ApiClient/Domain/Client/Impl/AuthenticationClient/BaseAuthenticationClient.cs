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

        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body)
        {
            return await Post<TResponse>(body, GetAuthHeaders());
        }

        protected async Task<GraphQLResponse<TResponse>> Post<TResponse>(string api, GraphQLRequest body)
        {
            return await Post<TResponse>(api, body, GetAuthHeaders());
        }

        private Dictionary<string, string> GetAuthHeaders()
        {
            return new Dictionary<string, string> { { "x_authing_app_id", AppId } };
        }
    }
}