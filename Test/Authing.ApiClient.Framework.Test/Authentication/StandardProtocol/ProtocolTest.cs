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
#if OIDC
            #region OIDC 登陆测试

            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("7gQ76fIZIsD5_YPiehDXt1xA78gVf3cCEzxkWhxG8YW");
            #endregion
#else
            #region OAUTH 登陆测试
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
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
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
#if OIDC
            #region OIDC 换取用户信息
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
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
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("e957ab20a5a202ce25de9583473304d335d4ade1");

            var result =
                await authenticationClient.GetNewAccessTokenByRefreshToken(res2.RefreshToken);
            Assert.NotNull(result);
        }

        [Fact]
        [Description("需要手动调试")]
        public async Task IntrospectToken()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
#if OIDC
            #region OIDC 检查Token
            //TODO:{"error":"invalid_client","error_description":"client authentication failed"}
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("Sl-IejBvixGlUL2EBfmJkFqfj3gRqxVBg3rXfOk4cQX");
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
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";

#if OIDC
            #region OIDC 校验Token
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("ICx2rvRQ03mN6m5E_Pry-a7fQ1p-ylmSDt0GUAKyf1G");
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
            //TODO:{"error":"invalid_client","error_description":"client authentication failed"}
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res1 = await authenticationClient.GetAccessTokenByCode("SWfxVr7B_6CC1q3EBBOjQuVtX__p_U7NatUC3i1VinE");
            var check = await authenticationClient.RevokeToken(res1.AccessToken);
        }
    }
}
