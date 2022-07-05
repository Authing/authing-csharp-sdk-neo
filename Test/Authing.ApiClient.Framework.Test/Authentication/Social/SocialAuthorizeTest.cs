using Authing.Library.Domain.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.Social
{
    public class SocialAuthorizeTest: BaseTest
    {
        [Fact]
        public async Task AuthorizeTest()
        {
           //var ss=await managementClient.Applications.FindByIdV2("62a9902a80f55c22346eb296");

            SocialAuthorizeOptions options = new SocialAuthorizeOptions 
            {
                Protocol="oidc",
            };

            socialAuthenticationClient.Authorize("adsadadadasasdadaas", options);  
        }
    }
}
