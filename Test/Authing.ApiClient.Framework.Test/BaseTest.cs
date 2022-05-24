using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Test.Base;

namespace Authing.ApiClient.Framework.Test
{
    public class BaseTest : TestBase
    {
        protected AuthenticationClient authenticationClient { get; set; }
        protected ManagementClient managementClient { get; set; }

        protected MfaAuthenticationClient mfaAuthenticationClient { get; set; }

        public BaseTest()
        {
            authenticationClient = new AuthenticationClient(
                opt =>
                {
                    opt.AppId = AppId;
                    opt.Host = Host;
                    opt.Secret = Secret;
                    opt.UserPoolId = UserPoolId;
                }
            );
            managementClient = new Domain.Client.Impl.ManagementBaseClient.ManagementClient(init: opt =>
            {
                opt.UserPoolId = UserPoolId;
                opt.Secret = Secret;
                opt.Host = Host;
            });

            mfaAuthenticationClient = new MfaAuthenticationClient(opt =>
            {
                opt.AppId = AppId;
                opt.Host = Host;
                opt.Secret = Secret;
                opt.UserPoolId = UserPoolId;
            });
        }
    }
}