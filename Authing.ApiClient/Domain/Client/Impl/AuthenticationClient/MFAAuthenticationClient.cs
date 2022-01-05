using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Interfaces.AuthenticationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Mfa;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public  class MfaAuthenticationClient : BaseAuthenticationClient, IMfaAuthenticationClient
    {
        public MfaAuthenticationClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
        }

        public async Task<List< IMfaAuthenticator>> GetMfaAuthenticators(GetMfaAuthenticatorsParam getMfaAuthenticatorsParam)
        {
            var result = await Get<GetMfaAuthenticatorsResponse>($"api/v2/mfa/authenticator/?type={getMfaAuthenticatorsParam.Type}&source={getMfaAuthenticatorsParam.TotpSource}", null).ConfigureAwait(false);
            return result.Data.Result;
        }

        public async Task<IMfaAssociation> AssosicateMfaAuthenticator(AssosicateMfaAuthenticatorParam parameter)
        {
            var result = await PostRaw<IMfaAssociation>("api/v2/mfa/totp/associate", parameter.ConvertJson(),parameter.MfaToken).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<CommonMessage> DeleteMfaAuthenticator()
        {
            var result = await Delete<CommonMessage>("api/v2/mfa/totp/associate",null).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<CommonMessage> ConfirmAssosicateMfaAuthenticator(ConfirmAssosicateMfaAuthenticatorParam parameter)
        {

            var result = await PostRaw<ConfirmAssosicateMfaAuthenticatorResponse>("api/v2/mfa/totp/associate/confirm", parameter.ConvertJson(),parameter.MfaToken).ConfigureAwait(false);

            return result.Data.Result;
        }

        public async Task<User> VerifyTotpMfa(VerifyTotpMfaParam verifyTotpMfaParam)
        {
            var result = await PostRaw<User>("api/v2/mfa/totp/verify", verifyTotpMfaParam.ConvertJson(), verifyTotpMfaParam.MfaToken).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<User> VerifyAppSmsMfa(VerifyAppSmsMfaParam verifyAppSmsMfaParam)
        {
            var result = await PostRaw<User>("api/v2/applications/mfa/sms/verify", verifyAppSmsMfaParam.ConvertJson(), verifyAppSmsMfaParam.MfaToken).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<User> VerifyAppEmailMfa(VerifyAppEmailMfaParam verifyAppEmailMfaParam)
        {
            var result = await PostRaw<User>("api/v2/applications/mfa/email/verify", verifyAppEmailMfaParam.ConvertJson(),verifyAppEmailMfaParam.MfaToken).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<bool> PhoneOrEmailBindable(PhoneOrEmailBindableParam phoneOrEmailBindableParam)
        {
            var result = await PostRaw<PhoneOrEmailBindableResponse>("api/v2/applications/mfa/check", phoneOrEmailBindableParam.ConvertJson(), phoneOrEmailBindableParam.MfaToken).ConfigureAwait(false);
            return result.Data.Result;
        }

        public async Task<User> VerifyTotpRecoveryCode(VerifyTotpRecoveryCodeParam verifyTotpRecoveryCodeParam)
        {
            var result = await PostRaw<User>("api/v2/mfa/totp/recovery", verifyTotpRecoveryCodeParam.ConvertJson(), verifyTotpRecoveryCodeParam.MfaToken).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<User> AssociateFaceByUrl(AssociateFaceByUrlParams options)
        {
            var res = await RequestCustomDataWithToken<RestfulResponse<User>>("api/v2/mfa/face/associate",
                options.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
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
                }, contenttype: ContentType.JSON).ConfigureAwait(false);
            return res.Data.Data;
        }

    }
}
