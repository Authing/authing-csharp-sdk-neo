using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public BaseAuthenticationClient(string appId)
        {
            this.AppId = appId;
        }

        public BaseAuthenticationClient(Action<InitAuthenticationClientOptions> init)
        {
        }

        protected async Task<TResponse> Post<TResponse>(GraphQLRequest body)
        {
            return await Post<TResponse>(body, GetAuthHeaders());
        }

        private Dictionary<string, string> GetAuthHeaders()
        {
            return new Dictionary<string, string> {{"x_authing_app_id", AppId}};
        }
    }
}