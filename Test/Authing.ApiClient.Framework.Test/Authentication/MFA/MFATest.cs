using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model.Authentication;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.MFA
{
    public class MFATest : BaseTest
    {
        [Fact]
        public async void VerifyEmailCode_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            try
            {
                var result = await loginClient.LoginByUsername("qidong5566", "12345678", null);
            }
            catch (AuthingException exp)
            {
                if (exp is AuthingException)
                {
                    ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>((exp as AuthingException).ResultData.ToString());

                    await loginClient.SendEmail("2481452007@qq.com", Types.EmailScene.MFA_VERIFY);
                }
            }

            var mfaResult = await client.VerifyAppEmailMfa(new Domain.Model.Authentication.VerifyAppEmailMfaParam { Code = "7335", Email = "2481452007@qq.com", MfaToken = ss.MfaToken });
        }

        [Fact]
        public async void Assoaticate_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            try
            {
                var result = await loginClient.LoginByUsername("qidong5566", "12345678", null);
            }
            catch (AuthingException exp)
            {
                if (exp is AuthingException)
                {
                    ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>((exp as AuthingException).ResultData.ToString());

                    //await loginClient.SendEmail("2481452007@qq.com", Types.EmailScene.MFA_VERIFY);
                }
            }

            var totpResult = await client.AssosicateMfaAuthenticator(new AssosicateMfaAuthenticatorParam { MfaToken = ss.MfaToken });

            var comfirmTotpResult = await client.ConfirmAssosicateMfaAuthenticator(new ConfirmAssosicateMfaAuthenticatorParam { Totp = "707610", MfaToken = ss.MfaToken });
        }

        [Fact]
        public async void AsssicateMfaConfirm_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            try
            {
                var result = await loginClient.LoginByUsername("qidong5566", "12345678", null);
            }
            catch (AuthingException exp)
            {
                if (exp is AuthingException)
                {
                    ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>((exp as AuthingException).ResultData.ToString());

                    //await loginClient.SendEmail("2481452007@qq.com", Types.EmailScene.MFA_VERIFY);
                }
            }

            var totpResult = await client.VerifyTotpMfa(new VerifyTotpMfaParam { Totp = "061448", MfaToken = ss.MfaToken });
        }

        [Fact]
        public async void VerifyAppSmsMfa_Test()
        {
            MFALoginResponse ss = null;
            var loginClient = authenticationClient;
            var client = mfaAuthenticationClient;
            try
            {
                var result = await loginClient.LoginByUsername("qidong5566", "12345678", null);
            }
            catch (AuthingException exp)
            {
                if (exp is AuthingException)
                {
                    ss = Newtonsoft.Json.JsonConvert.DeserializeObject<MFALoginResponse>((exp as AuthingException).ResultData.ToString());

                    //await loginClient.SendEmail("2481452007@qq.com", Types.EmailScene.MFA_VERIFY);
                    await loginClient.SendSmsCode("13348926753");
                }
            }

            var totpResult = await client.VerifyAppSmsMfa(new VerifyAppSmsMfaParam { Code = "3365", Phone = "13348926753", MfaToken = ss.MfaToken });
        }
    }
}