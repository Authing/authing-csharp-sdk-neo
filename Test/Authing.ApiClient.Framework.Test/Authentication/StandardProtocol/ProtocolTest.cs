using System;
using System.Collections.Generic;
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
        public async Task buildAuthorizeUrlTest()
        {
            authenticationClient.Options.Protocol = Protocol.SAML;
            string saml = authenticationClient.BuildAuthorizeUrl(new SamlOption());
            var url = new Uri(saml);
            Assert.Equal(url.AbsolutePath,$@"/api/v2/saml-idp/{AppId}");
            //TODO:需要核实
            authenticationClient.Options.Protocol = Protocol.OIDC;
            string oidc = authenticationClient.BuildAuthorizeUrl(new OidcOption(){ RedirectUri = "www.baidu.com",Nonce = "nonce test"});
            url = new Uri(oidc);
            //TODO:需要核实
            authenticationClient.Options.Protocol = Protocol.OAUTH;
            string oauth = authenticationClient.BuildAuthorizeUrl(new OauthOption() { RedirectUri = "www.baidu.com" });
            url = new Uri(oauth);
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
            authenticationClient.Options.RedirectUri = "www.baidu.com";
            authenticationClient.Options.TokenEndPointAuthMethod = TokenEndPointAuthMethod.NONE;
            var res = await authenticationClient.GetAccessTokenByCode("aNhjg8hc__G8vd7LbO5ZV_hWIzP1BN6KVYpcei1XiOn");
        }

        [Fact]
        public async Task GetUserInfoByAccessTokenTest()
        {
        }
    }
}
