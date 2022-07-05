using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Authing.ApiClient.Extensions;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
    public class SocialAuthenticationClient: BaseAuthenticationClient
    {

        public SocialAuthenticationClient(Action<InitAuthenticationClientOptions> init):base(init)
        {

        }

        /// <summary>
        /// 社会化登录认证
        /// </summary>
        /// <param name="provider">社会化登录的唯一标识</param>
        /// <param name="options">登录选项</param>
        /// <returns>返回授权的链接</returns>
        public string  Authorize(string provider,SocialAuthorizeOptions options)
        {
            if (string.IsNullOrEmpty(options.UUID))
            { 
                options.UUID= AuthingUtils.GenerateRandomString(20);
            }

            Dictionary<string, string> query = new Dictionary<string, string>
            {
                { "from_guard","1"},
                { "app_id",AppId},
                { "authorization_params",options.Authiorization_Params==null?
                                            options.AuthorizationParams.ConvertJson():
                                            options.Authiorization_Params.ConvertJson()},
                { "with_identities",options.WithIdentities?"1":"0"},
                { "with_custom_data",options.WithCustomData?"1":"0"},
                { "protocol","oidc"},
                { "uuid",options.UUID}
            };

            if (options.Context != null)
            {
                query.Add("context",options.Context.ConvertJson());
            }

            if (!string.IsNullOrWhiteSpace(options.TenantId))
            {
                query.Add("tenant_id", options.TenantId);
            }

            if (options.CustomData != null)
            {
                query.Add("custom_data", options.CustomData.ConvertJson());
            }

            string url = $"{Host}/connections/social/{provider}?{AuthingUtils.CreateQueryParams(query)}";

            return url;
        }
    }
}
