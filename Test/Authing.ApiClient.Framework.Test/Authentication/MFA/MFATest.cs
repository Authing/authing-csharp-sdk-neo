using System.Linq;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.MFA
{
    public class MFATest : BaseTest
    {
        /// <summary>
        /// 2022-8-15 测试通过
        /// </summary>
        [Fact]
        public async void VerifyEmailCode_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            AuthingErrorBox box = new AuthingErrorBox();
            var result = await loginClient.LoginByUsername("tmgg", "88886666", null, box);
            if (box.Value.Any())
            {
                ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>(box.Value.First().Message.Data.ToString());
            }


            await loginClient.SendEmail("574378328@qq.com", Types.EmailScene.MFA_VERIFY);

            var mfaResult = await client.VerifyAppEmailMfa(new Domain.Model.Authentication.VerifyAppEmailMfaParam { Code = "9144", Email = "574378328@qq.com", MfaToken = ss.MfaToken });
        }

        /// <summary>
        /// 2022-8-15 测试不通过
        /// </summary>
        [Fact]
        public async void Assoaticate_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            AuthingErrorBox box = new AuthingErrorBox();

            var result = await loginClient.LoginByUsername("qidong11233", "3866364", null, box);
            ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>(box.Value.First().Message.Data.ToString());

            var totpResult = await client.AssosicateMfaAuthenticator(new AssosicateMfaAuthenticatorParam { MfaToken = ss.MfaToken });

            var comfirmTotpResult = await client.ConfirmAssosicateMfaAuthenticator(new ConfirmAssosicateMfaAuthenticatorParam { Totp = "151557", MfaToken = ss.MfaToken });
        }

        /// <summary>
        /// 2022-8-15 测试通过
        /// </summary>
        [Fact]
        public async void AsssicateMfaConfirm_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            AuthingErrorBox box = new AuthingErrorBox();

            var result = await loginClient.LoginByUsername("tmgg", "88886666", null, box);

            ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>(box.Value.First().Message.Data.ToString());

            var totpResult = await client.VerifyTotpMfa(new VerifyTotpMfaParam { Totp = "517842", MfaToken = ss.MfaToken });
        }

        /// <summary>
        /// 2022-8-15 测试通过
        /// </summary>
        [Fact]
        public async void VerifyAppSmsMfa_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            AuthingErrorBox box = new AuthingErrorBox();
            var result = await loginClient.LoginByUsername("tmgg", "88886666", null,box);
            ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>(box.Value.First().Message.Data.ToString());
            await loginClient.SendSmsCode("17665662048");

            var totpResult = await client.VerifyAppSmsMfa(new VerifyAppSmsMfaParam { Code = "1452", Phone = "17665662048", MfaToken = ss.MfaToken });
        }
    }
}