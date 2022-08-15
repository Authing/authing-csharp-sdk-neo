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
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public class MfaAuthenticationClient : BaseAuthenticationClient, IMfaAuthenticationClient
    {
        public MfaAuthenticationClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
        }

        /// <summary>
        /// 获取 MFA 认证器
        /// </summary>
        /// <param name="getMfaAuthenticatorsParam"></param>
        /// <returns></returns>
        public async Task<List<IMfaAuthenticator>> GetMfaAuthenticators(GetMfaAuthenticatorsParam getMfaAuthenticatorsParam, AuthingErrorBox authingErrorBox = null)
        {
            var result = await RequestCustomDataWithOutToken<GetMfaAuthenticatorsResponse>($"api/v2/mfa/authenticator/?type={getMfaAuthenticatorsParam.Type}" +
                $"&source={getMfaAuthenticatorsParam.TotpSource}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data.Result;
        }

        /// <summary>
        /// 请求 MFA 二维码和密钥信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IMfaAssociation> AssosicateMfaAuthenticator(AssosicateMfaAuthenticatorParam parameter, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<IMfaAssociation>("api/v2/mfa/totp/associate", parameter.ConvertJson(), parameter.MfaToken).ConfigureAwait(false);

            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        ///解绑 MFA
        /// </summary>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteMfaAuthenticator(AuthingErrorBox authingErrorBox = null)
        {
            var result = await RequestCustomDataWithToken<CommonMessage>("api/v2/mfa/totp/associate", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        /// 确认绑定 MFA
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<CommonMessage> ConfirmAssosicateMfaAuthenticator(ConfirmAssosicateMfaAuthenticatorParam parameter, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<ConfirmAssosicateMfaAuthenticatorResponse>("api/v2/mfa/totp/associate/confirm", parameter.ConvertJson(), parameter.MfaToken).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data.Result;
        }

        /// <summary>
        /// 检验二次验证 MFA 口令
        /// </summary>
        /// <param name="verifyTotpMfaParam"></param>
        /// <returns></returns>
        public async Task<User> VerifyTotpMfa(VerifyTotpMfaParam verifyTotpMfaParam, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<User>("api/v2/mfa/totp/verify", verifyTotpMfaParam.ConvertJson(), verifyTotpMfaParam.MfaToken).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        /// 检验二次验证 MFA 短信验证码
        /// </summary>
        /// <param name="verifyAppSmsMfaParam"></param>
        /// <returns></returns>
        public async Task<User> VerifyAppSmsMfa(VerifyAppSmsMfaParam verifyAppSmsMfaParam, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<User>("api/v2/applications/mfa/sms/verify", verifyAppSmsMfaParam.ConvertJson(), verifyAppSmsMfaParam.MfaToken).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        /// 检验二次验证 MFA 邮箱验证码
        /// </summary>
        /// <param name="verifyAppEmailMfaParam"></param>
        /// <returns></returns>
        public async Task<User> VerifyAppEmailMfa(VerifyAppEmailMfaParam verifyAppEmailMfaParam, AuthingErrorBox authingErrorBox = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>() { { "Authorization", "bearer " + verifyAppEmailMfaParam.MfaToken } };
            var result = await RequestCustomDataWithToken<User>("api/v2/applications/mfa/email/verify", verifyAppEmailMfaParam.ConvertJson(), headers, contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        /// 检测手机号或邮箱是否已被绑定
        /// </summary>
        /// <param name="phoneOrEmailBindableParam"></param>
        /// <returns></returns>
        public async Task<bool> PhoneOrEmailBindable(PhoneOrEmailBindableParam phoneOrEmailBindableParam, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<PhoneOrEmailBindableResponse>("api/v2/applications/mfa/check", phoneOrEmailBindableParam.ConvertJson(), phoneOrEmailBindableParam.MfaToken).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data.Result;
        }

        /// <summary>
        /// 检验二次验证 MFA 恢复代码
        /// </summary>
        /// <param name="verifyTotpRecoveryCodeParam"></param>
        /// <returns></returns>
        public async Task<User> VerifyTotpRecoveryCode(VerifyTotpRecoveryCodeParam verifyTotpRecoveryCodeParam, AuthingErrorBox authingErrorBox = null)
        {
            var result = await PostRaw<User>("api/v2/mfa/totp/recovery", verifyTotpRecoveryCodeParam.ConvertJson(), verifyTotpRecoveryCodeParam.MfaToken).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        /// <summary>
        /// 通过图片 URL 绑定人脸
        /// TODO:未测试
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<User> AssociateFaceByUrl(AssociateFaceByUrlParams options, AuthingErrorBox authingErrorBox = null)
        {
            var res = await RequestCustomDataWithToken<RestfulResponse<User>>("api/v2/mfa/face/associate",
                options.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Data;
        }

        /// <summary>
        /// 人脸二次认证
        /// TODO:未测试
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="mfaToken"></param>
        /// <returns></returns>
        public async Task<User> VerifyFaceMfa(string photo, string mfaToken, AuthingErrorBox authingErrorBox = null)
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
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Data;
        }
    }
}