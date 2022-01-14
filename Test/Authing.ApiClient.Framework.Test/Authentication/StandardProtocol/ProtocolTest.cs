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
        public ProtocolTest()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task BuildAuthorizeUrlTest()
        {
            authenticationClient.Options.Protocol = Protocol.SAML;
            string saml = authenticationClient.BuildAuthorizeUrl(new SamlOption());
            authenticationClient.Options.Protocol = Protocol.OIDC;
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task BuildLogoutUrl()
        {
#if OIDC
            #region OIDC 登出
            string oidcurl = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res = await authenticationClient.GetAccessTokenByCode("pMXHOFunjq02nJL9Zpb6jQRjra-FZRNYPocAj1HtWcs");
            string oidcout = authenticationClient.BuildLogoutUrl(new LogoutParams() { RedirectUri = "https://www.baidu.com", IdToken = res.IdToken });
            #endregion
#else
            #region OAuth 登出
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauthurl = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("66b9ce990fe918953d6745faff73a7be1f65dce9");
            string oauthout = authenticationClient.BuildLogoutUrl(new LogoutParams() { RedirectUri = "https://www.baidu.com", IdToken = res.IdToken });
            #endregion
#endif
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetAccessTokenByCodeTest()
        {
#if OIDC
            #region OIDC 登陆测试

            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res = await authenticationClient.GetAccessTokenByCode("7gQ76fIZIsD5_YPiehDXt1xA78gVf3cCEzxkWhxG8YW");
            #endregion
#else
            #region OAUTH 登陆测试
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("45aae0297823723f4b8081e88cc8959d5c1d7e77");
            #endregion
#endif
            Assert.NotNull(res);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetUserInfoByAccessTokenTest()
        {
#if OIDC
            #region OIDC 换取用户信息
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res = await authenticationClient.GetAccessTokenByCode("PqRrKsGlPzZEabb4OTV_OKZVzeO1-KCDNXpyDgdQwnJ");
            var result = await authenticationClient.GetUserInfoByAccessToken(res.AccessToken);
            #endregion
#else
            #region OAuth 换取用户信息
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("e36b7fb15666ef0a24cc7e9869ff882309927692");
            var result = await authenticationClient.GetUserInfoByAccessToken(res.AccessToken);
            #endregion
#endif
            Assert.NotNull(result);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task GetNewAccessTokenByRefreshToken()
        {
#if OIDC
            #region OIDC 刷新Token
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res2 = await authenticationClient.GetAccessTokenByCode("TFjR_hamTzjrmsAnlWI3qxkpxJRxnnNZ0zCb0ZUplq8");
            var result = await authenticationClient.GetNewAccessTokenByRefreshToken(res2.RefreshToken);
            #endregion
#else
            #region OAuth 刷新Token
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("01ed0aa1127edd9cecb85581fec73ebe6b3ed34f");
            var result = await authenticationClient.GetNewAccessTokenByRefreshToken(res2.RefreshToken);
            #endregion
#endif


            Assert.NotNull(result);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task IntrospectToken()
        {
#if OIDC
            #region OIDC 检查Token
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res1 = await authenticationClient.GetAccessTokenByCode("ZvtfRZ82l2oM52V0GgUr3qpRd-hq0kfUg6xa8zqzpVE");
            var check = await authenticationClient.IntrospectToken(res1.AccessToken);
            #endregion
#else
            #region OAUTH 检查Token
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("1f4fda40bb1533a6e592a5f5f96c4492f77a703b");
            var check = await authenticationClient.IntrospectToken(res2.AccessToken);
            #endregion
#endif
            Assert.True(check.Active);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task ValidateToken()
        {
#if OIDC
            #region OIDC 校验Token
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res1 = await authenticationClient.GetAccessTokenByCode("QxhFQB3Cm9pT2Mb-R_9Wc2xyBIrufyhBqbvVPduzEqJ");
            var check = await authenticationClient.ValidateToken(
                new ValidateTokenParams()
                { AccessToken = res1.AccessToken }
            );
            #endregion

#else
            #region OAuth 校验Token
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("c7b0edcbcf49cb264012e42d59d39e3dc2fa78b7");
            var check = await authenticationClient.ValidateToken(
                new ValidateTokenParams()
                { AccessToken = res2.AccessToken }
            );
            #endregion
#endif
            Assert.NotNull(check);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task RevokeToken()
        {
#if OIDC
            #region OIDC 撤回
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com", Scope = "openid profile email phone address offline_access" });
            var res1 = await authenticationClient.GetAccessTokenByCode("T42LnZgbbQsCh8u2shqy6WLF92dwOCioWgf_sbuT7d2");
            var check = await authenticationClient.RevokeToken(res1.AccessToken);
            #endregion
#else

            #region OAuth 撤回
            //TODO:测试不通过
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("767b6e8fd032e931307680a62e7fdb3914e2ae16");
            var check = await authenticationClient.RevokeToken(res2.AccessToken);
            #endregion
#endif
            Assert.NotNull(check);

        }
    }
}
