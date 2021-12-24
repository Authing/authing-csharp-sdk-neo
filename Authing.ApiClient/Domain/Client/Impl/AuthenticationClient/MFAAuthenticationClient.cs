using Authing.ApiClient.Interfaces.AuthenticationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Mfa;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public partial class AuthenticationClient : BaseAuthenticationClient, IAuthenticationClient
    {

        public async Task<User> AssociateFaceByUrl(AssociateFaceByUrlParams options)
        {
            var res = await RequestCustomDataWithToken<RestfulResponse<User>>("api/v2/mfa/face/associate",
                options.ConvertJson(), contenttype: ContentType.JSON);
            return res.Data.Data;
        }

        public async Task<User> VerifyFaceMfa(string photo, string mfaToken)
        {
            var res = await RequestCustomDataWithToken<RestfulResponse<User>>("api/v2/mfa/face/verify",
                new Dictionary<string, string>()
                {
                    { "photo", photo }
                }.ConvertJson(),
                new Dictionary<string, string>()
                {
                    {
                        "Authorization",
                        $"Basic{mfaToken}"
                    }
                }, contenttype: ContentType.JSON);
            return res.Data.Data;
        }

    }
}
