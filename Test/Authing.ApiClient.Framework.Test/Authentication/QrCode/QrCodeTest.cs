using Authing.Library.Domain.Model.Authentication;
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
            var result =await qrCodeAuthenticationClient.GeneCode(new GeneQrCodeParam { AutoMergeQrCode=false,Scene=QrCodeScene.APP_AUTH});

            Assert.NotNull(result);
        }

        [Fact]
        public async Task QRCodeStatusCheckTest()
        {
            var result = await qrCodeAuthenticationClient.CheckStatus("F0QFtziVP").ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
