namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        protected static string UserPoolId { get; set; } = "6195ebce748aef66dc0b612d";
        protected static string Secret { get; set; } = "4cb790e83a392f9ad8ee227e9e879839";
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