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
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Mfa;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public partial class AuthenticationClient : BaseAuthenticationClient, IAuthenticationClient
    {
        public async Task<List< IMfaAuthenticator>> GetMfaAuthenticators(GetMfaAuthenticatorsParam getMfaAuthenticatorsParam)
        {
            var result = await Get<GetMfaAuthenticatorsResponse>($"api/v2/mfa/authenticator/?type={getMfaAuthenticatorsParam.Type}&source={getMfaAuthenticatorsParam.TotpSource}", null);

            //var res = await Host.AppendPathSegment("api/v2/mfa/authenticator").WithOAuthBearerToken(MFAToken).SetQueryParams(new
            //{
            //    type = getMfaAuthenticatorsParam.Type,
            //    source = getMfaAuthenticatorsParam.Source
            //}).GetJsonAsync<IMfaAuthenticator[]>(cancellationToken);
            return result.Data.Result;
        }

        public async Task<IMfaAssociation> AssosicateMfaAuthenticator(AssosicateMfaAuthenticatorParam parameter)
        {
            var result = await PostRaw<AssosicateMfaAuthenticatorResponse>("api/v2/mfa/totp/associate", parameter.ConvertJson());

            //var res = await Host.AppendPathSegment("api/v2/mfa/totp/associate").WithOAuthBearerToken(MFAToken).PostJsonAsync(new
            //{
            //    authenticator_type = parameter.AuthenticatorType,
            //    source = parameter.Source
            //}, cancellationToken).ReceiveJson<AssosicateMfaAuthenticatorRes>();
            return result.Data.Result;
        }

        public async Task<CommonMessage> DeleteMfaAuthenticator()
        {
            var result = await Delete<CommonMessage>("api/v2/mfa/totp/associate", null);

            return result.Data;
            //var res = await Host.AppendPathSegment("api/v2/mfa/totp/associate").WithOAuthBearerToken(MFAToken).DeleteAsync();
            //return new CommonMessage
            //{
            //    Code = 200,
            //    Message = "TOTP MFA 解绑成功"
            //};
        }

        public async Task<CommonMessage> ConfirmAssosicateMfaAuthenticator(ConfirmAssosicateMfaAuthenticatorParam parameter)
        {

            var result = await PostRaw<ConfirmAssosicateMfaAuthenticatorResponse>("api/v2/mfa/totp/associate/confirm", parameter.ConvertJson());

            return result.Data.Result;
            
            //var res = await Host.AppendPathSegment("api/v2/mfa/totp/associate/confirm").WithOAuthBearerToken(parameter.MFAToken).PostJsonAsync(
            //    new
            //    {
            //        authenticator_type = parameter.AuthenticatorType,
            //        totp = parameter.Totp,
            //        source = parameter.Source
            //    },
                
            //);
            //return new CommonMessage
            //{
            //    Code = 200,
            //    Message = "TOTP MFA 绑定成功"
            //};
        }

        public async Task<User> VerifyTotpMfa(VerifyTotpMfaParam verifyTotpMfaParam)
        {
            var result = await PostRaw<VerifyTotpMfaResponse>("api/v2/mfa/totp/verify", verifyTotpMfaParam.ConvertJson());

            return result.Data.Result;
            //var res = await Host.AppendPathSegment("api/v2/mfa/totp/verify").WithOAuthBearerToken(verifyTotpMfaParam.MFAToken).PostJsonAsync(new
            //{
            //    totp = verifyTotpMfaParam.Totp
            //}, cancellationToken).ReceiveJson<User>();
            //return res;

        }

        public async Task<User> VerifyAppSmsMfa(VerifyAppSmsMfaParam verifyAppSmsMfaParam)
        {
            var result = await PostRaw<VerifyAppSmsMfaResponse>("api/v2/applications/mfa/sms/verify", verifyAppSmsMfaParam.ConvertJson());
            return result.Data.Result;

            //var res = await Host.AppendPathSegment("api/v2/applications/mfa/sms/verify").WithOAuthBearerToken(verifyAppSmsMfaParam.MFAToken).PostJsonAsync(new
            //{
            //    phone = verifyAppSmsMfaParam.Phone,
            //    code = verifyAppSmsMfaParam.Code
            //}, cancellationToken).ReceiveJson<User>();
            //return res;
        }

        public async Task<User> VerifyAppEmailMfa(VerifyAppEmailMfaParam verifyAppEmailMfaParam)
        {
            var result = await PostRaw<VerifyAppEmailMfaResponse>("api/v2/applications/mfa/email/verify", verifyAppEmailMfaParam.ConvertJson());
            return result.Data.Result;

            //var res = await Host.AppendPathSegment("api/v2/applications/mfa/email/verify").WithOAuthBearerToken(verifyAppEmailMfaParam.MFAToken).PostJsonAsync(new
            //{
            //    email = verifyAppEmailMfaParam.Email,
            //    code = verifyAppEmailMfaParam.Code
            //}, cancellationToken).ReceiveJson<User>();
            //return res;
        }

        public async Task<bool> PhoneOrEmailBindable(PhoneOrEmailBindableParam phoneOrEmailBindableParam)
        {
            var result = await PostRaw<PhoneOrEmailBindableResponse>("api/v2/applications/mfa/check", phoneOrEmailBindableParam.ConvertJson());
            return result.Data.Result;

            //var res = await Host.AppendPathSegment("api/v2/applications/mfa/check").WithOAuthBearerToken(phoneOrEmailBindableParam.MFAToken).PostJsonAsync(
            //    new
            //    {
            //        email = phoneOrEmailBindableParam.Email,
            //        phone = phoneOrEmailBindableParam.Phone
            //    },
            //    cancellationToken
            //).ReceiveJson<bool>();
            //return res;
        }

        public async Task<User> VerifyTotpRecoveryCode(VerifyTotpRecoveryCodeParam verifyTotpRecoveryCodeParam)
        {
            var result = await PostRaw<VerifyTotpRecoveryCodeResponse>("api/v2/mfa/totp/recovery", verifyTotpRecoveryCodeParam.ConvertJson());
            return result.Data.Result;

            //var res = await Host.AppendPathSegment("api/v2/mfa/totp/recovery").WithOAuthBearerToken(verifyTotpRecoveryCodeParam.MFAToken).PostJsonAsync(new
            //{
            //    recoveryCode = verifyTotpRecoveryCodeParam.RecoveryCode
            //}, cancellationToken).ReceiveJson<User>();
            //return res;
        }

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
