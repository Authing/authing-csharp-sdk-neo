using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Types;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.StandardProtocol
{
    public class ProtocolTest : BaseTest
    {
        [Fact]
        public async Task BuildAuthorizeUrlTest()
        {
            authenticationClient.Options.Protocol = Protocol.SAML;
            string saml = authenticationClient.BuildAuthorizeUrl(new SamlOption());
            //TODO:需要核实
            authenticationClient.Options.Protocol = Protocol.OIDC;
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption(){ RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a"});
            var url2 = new Uri(oidc);
            //TODO:需要核实
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
        }
        [Fact]
        public async Task GetAccessTokenByCodeTest()
        {
            //TODO:无法调试
            /*
             * {
               "error": "未知错误",
               "error_description": "Cannot read property 'split' of undefined"
               }
             */
            authenticationClient.Options.AppId = AppId;
            authenticationClient.Options.Secret = Secret;
            authenticationClient.Options.UserPoolId = UserPoolId;
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
            authenticationClient.Options.RedirectUri =
                "https://console.authing.cn/console/get-started/6172807001258f603126a78a";
            //authenticationClient.Options.TokenEndPointAuthMethod = TokenEndPointAuthMethod.CLIENT_SECRET_POST;
            var res = await authenticationClient.GetAccessTokenByCode("szNqV433L1TLv91Vc0LVyKtm6T3TPIyC9a8gFRDop_h");
            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetUserInfoByAccessTokenTest()
        {
            //TODO:返回错误 {"error":"invalid_request","error_description":"access token must only be provided using one mechanism"}
            var res = await authenticationClient.LoginByUsername("tm574378328", "123456",false);
            var result  = await authenticationClient.GetUserInfoByAccessToken(res.Token);
        }

        [Fact]
        public async Task GetNewAccessTokenByRefreshToken()
        {
            authenticationClient.Options.Protocol = Protocol.OIDC;
            var res = await authenticationClient.LoginByUsername("tm574378328", "123456", false);
            var data = await authenticationClient.GetAccessTokenByCode("0c273f1b8393487e4fb72474d43e1464636b8d97&state=6gA3t-HWO");

            var result =
                await authenticationClient.GetNewAccessTokenByRefreshToken(data.RefreshToken);
        }
    }
}
