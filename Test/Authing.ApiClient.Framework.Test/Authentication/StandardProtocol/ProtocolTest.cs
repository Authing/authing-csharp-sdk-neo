using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Model.Management.Applications;
using Authing.ApiClient.Types;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.StandardProtocol
{
    public class ProtocolTest : BaseTest
    {
        [Fact]
        [Description("需要手动调试")]
        public async Task BuildAuthorizeUrlTest()
        {
            authenticationClient.Options.Protocol = Protocol.SAML;
            string saml = authenticationClient.BuildAuthorizeUrl(new SamlOption());
            authenticationClient.Options.Protocol = Protocol.OIDC;
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
            var url2 = new Uri(oidc);
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetAccessTokenByCodeTest()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            #region OIDC登陆测试
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("7gQ76fIZIsD5_YPiehDXt1xA78gVf3cCEzxkWhxG8YW");
            #endregion

            #region OAUTH登陆测试
            //authenticationClient.Options.Protocol = Protocol.OAUTH;
            //string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            //var res2 = await authenticationClient.GetAccessTokenByCode("45aae0297823723f4b8081e88cc8959d5c1d7e77");
            #endregion

            Assert.NotNull(res1);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetUserInfoByAccessTokenTest()
        {
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("qb8CKNnJ88PN1OMJ_d3gWq4zsiamgAx5xC58N8PepLX");
            var result = await authenticationClient.GetUserInfoByAccessToken(res.AccessToken);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetNewAccessTokenByRefreshToken()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("e957ab20a5a202ce25de9583473304d335d4ade1");

            var result =
                await authenticationClient.GetNewAccessTokenByRefreshToken(res2.RefreshToken);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task IntrospectToken()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            #region OIDC登陆测试
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("p-wiYGdGab2BoNcmmfLp1b4p844WPjrAJ4YIgteVurd");
            var check = await authenticationClient.IntrospectToken(res1.AccessToken);
            //TODO:{"error":"invalid_client","error_description":"client authentication failed"}
            Assert.True(check.Active);
            #endregion


            #region OAUTH登陆测试
            //authenticationClient.Options.Protocol = Protocol.OAUTH;
            //string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            //var res2 = await authenticationClient.GetAccessTokenByCode("352959d9a7f9270c13f35776989474e51f6031f9");
            //var check = await authenticationClient.IntrospectToken(res2.AccessToken);
            //Assert.True(check.Active);
            #endregion
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task ValidateToken()
        {
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("ICx2rvRQ03mN6m5E_Pry-a7fQ1p-ylmSDt0GUAKyf1G");
            var check = await authenticationClient.ValidateToken(
                new ValidateTokenParams()
                { AccessToken = res1.AccessToken}
                );
            Assert.NotNull(check);
        }
    }
}
