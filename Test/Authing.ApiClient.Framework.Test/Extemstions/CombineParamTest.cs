using Authing.ApiClient.Extensions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Extemstions
{
    public class CombineParamTest
    {
        [Fact]
        public void CombineParamTeststring()
        {
            string host = "http://www.baidu.com";

            var combineQueryParams = host + new { test1 = 1, test2 = 2 }.Convert2QueryParams();
            Assert.Equal(combineQueryParams, "http://www.baidu.com?test1=1&test2=2");

            //var result = ReflectionHelper.GetInputObjec(new { test1 = 1,test2 = 2,test3 = 3 });
        }
    }
}