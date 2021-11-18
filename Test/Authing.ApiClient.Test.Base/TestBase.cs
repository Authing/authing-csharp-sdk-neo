namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        protected static string UserPoolId { get; set; } = "6195ffe51c963afd95523a6e";
        protected static string Secret { get; set; } = "abe87c7ade13c670d255427909429e7d";
        protected static string AppId { get; set; } = "6195ffe65fea96703010878a";
        protected static string TestUserId = "6195ffe645945c01d7ba9c94";
#if TEST_ENV
        public static string Host { get; set; } = "https://core.test.authing-inc.co";
#elif DEV_ENV
        public static string Host { get; set; } = "https://core.dev.authing-inc.co";
#else
        public static string Host { get; set; } = "https://core.authing.cn";
#endif
    }
}