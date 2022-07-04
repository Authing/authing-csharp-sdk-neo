using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Domain.Model.Management.Applications;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Authentication;
using Authing.Library.Domain.Model.Management.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
    public class EnterpriseAuthenticationClient: BaseAuthenticationClient
    {
        public EnterpriseAuthenticationClient(Action<InitAuthenticationClientOptions> init):base(init)
        {

        }

        public async Task Authorize(Protocol protocol, string identifier, EnterpriseAuthenticationOptions options)
        {
            string state = ApiClient.Domain.Utils.AuthingUtils.GenerateRandomString(32);

            if (protocol == Protocol.OIDC) 
            {
                await RequestCustomDataWithOutToken<object>("api/v2/connections/oidc/start-interaction", new Dictionary<string, string>
                {
                    {"state",state },
                   { "protocol",protocol.ToString().ToLower()},
                   { "userPoolId",UserPoolId},
                   { "appId",""},
                   { "referer",""},
                   { "connection",string.Format("{\"providerIentifier\":\"{0}\"}",identifier)}
                }.ConvertJson(),
                     method: System.Net.Http.HttpMethod.Post, contenttype: ContentType.JSON).ConfigureAwait(false) ;
            }

            var appConfigInfo = await InitProviderContext(AppId).ConfigureAwait(false);

            //appConfigInfo.IdentityProviders.

            //string url=
        }

        private async Task<ApplicationV2> InitProviderContext(string appId)
        {
            var res = await RequestCustomDataWithOutToken<ApplicationV2>($"api/v2/applications/{appId}/public-config", method: HttpMethod.Get).ConfigureAwait(false);
            return res.Data;
        }

    }
}
