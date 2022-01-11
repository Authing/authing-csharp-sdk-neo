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
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
            var url2 = new Uri(oidc);
            //TODO:需要核实
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://console.authing.cn/console/get-started/6172807001258f603126a78a" });
        }
        [Fact]
        public async Task GetAccessTokenByCodeTest()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            #region OIDC登陆测试
            //string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption(){"https://www.baidu.com"});
            //var res1 = await authenticationClient.GetAccessTokenByCode("hUufWdiXuzSSXNyb1bRIx_iPymly3KSJ02SJvdIg0ZU");
            #endregion

            #region OAUTH登陆测试

            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("45aae0297823723f4b8081e88cc8959d5c1d7e77");
            #endregion

            Assert.NotNull(res2);
        }

        [Fact]
        public async Task GetUserInfoByAccessTokenTest()
        {
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption() { RedirectUri = "https://www.baidu.com" });
            var res = await authenticationClient.GetAccessTokenByCode("qb8CKNnJ88PN1OMJ_d3gWq4zsiamgAx5xC58N8PepLX");
            var result = await authenticationClient.GetUserInfoByAccessToken(res.AccessToken);
        }

        [Fact]
        public async Task GetNewAccessTokenByRefreshToken()
        {
            authenticationClient.Options.RedirectUri = "https://www.baidu.com";
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "https://www.baidu.com" });
            var res2 = await authenticationClient.GetAccessTokenByCode("e957ab20a5a202ce25de9583473304d335d4ade1");

            var result =
                await authenticationClient.GetNewAccessTokenByRefreshToken(res2.RefreshToken);
        }
    }
}
