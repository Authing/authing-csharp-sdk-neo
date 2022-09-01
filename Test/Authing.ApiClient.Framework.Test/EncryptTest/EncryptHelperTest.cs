using Authing.ApiClient.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authing.ApiClient.Framework.Test.EncryptTest
{

    public class EncryptHelperTest
    {
        [Fact]
        public void JWKSEncryptTest()
        {
            string json = @"{""keys"":[{""kty"":""RSA"",""e"":""AQAB"",""n"":""yqwm9vUP8rPYFsHHsqXFwwWtXZMpWF-7DYKdTFxJEBSpykb30_QJ_YAs3TpbjK_VwbCtuAz1oR0ErPJDCLw9q6h-i1NDOynW5gLGVt9NBmnBrAUc6lhtI-w-46RS2IS6xG8fNL59YSI5eus6ID9JxT56AJ2Cwk8GYJoB4KUUxPTC5Ez5FdOR3h0Krv0UlxcZj7We1MBghyEJi8jdyRphQuM8b0uK9lxYSmRF4KHwxdopM_A6kDOQzhNf-vvxBEmD2kSoduGQJKNI0UGD5w4GtORiyhbr9b8C3XCsM1QaoPz_iEd2Ma4HduX0VvmexsyvUVpYLXVXIpUTf4JzP-TscQ"",""alg"":""RS256"",""use"":""sig"",""kid"":""zEq_F6GjuWg93QsBlrvZP7twV9-Eqe3qCCCDZXevBH0""}]}";
            string publicKey= EncryptHelper.GetPublickeyFromJson(json);

            var ss= EncryptHelper.RsaEncryptWithJWKs("3866364", publicKey);
        }
    }
}
