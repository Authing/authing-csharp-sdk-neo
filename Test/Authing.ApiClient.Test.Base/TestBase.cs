namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        protected static string UserPoolId { get; set; } = "613189b2eed393affbbf396e";
        protected static string Secret { get; set; } = "ccf4951a33e5d54d64e145782a65f0a7";
        protected static string AppId { get; set; } = "613189b38b6c66cac1d211bd";
        protected static string TestUserId = "6195ffe645945c01d7ba9c94";
#if TEST_ENV
        public static string Host { get; set; } = "https://core.authing.cn";
#elif DEV_ENV
        public static string Host { get; set; } = "https://core.dev.authing-inc.co";
#else
        public static string Host { get; set; } = "https://core.authing.cn";
#endif
    }
}