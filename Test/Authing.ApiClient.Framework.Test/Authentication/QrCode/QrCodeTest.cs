using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication.QrCode
{
    public class QrCodeTest : BaseTest
    {
        [Fact]
        public async Task GeneCodeTest()
        {
            var result =await qrCodeAuthenticationClient.GeneCode(new Library.Domain.Model.Authentication.GeneQrCodeParam { AutoMergeQrCode=false,Scene=Library.Domain.Model.Authentication.QrCodeScene.APP_AUTH});

            Assert.NotNull(result);
        }
    }
}
