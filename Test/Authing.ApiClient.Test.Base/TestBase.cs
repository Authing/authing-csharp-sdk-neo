namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        //protected static string UserPoolId { get; set; } = "6195ebce748aef66dc0b612d";
        //protected static string Secret { get; set; } = "4cb790e83a392f9ad8ee227e9e879839";
        //protected static string AppId { get; set; } = "6195ebcf5255f3d735ba9063";
        //protected static string TestUserId = "6195ffe645945c01d7ba9c94";

        //protected static string TestUserId = "61a82941979c96c04ed9e920";

        protected static string UserPoolId { get; set; } = "617280674680a6ca2b1f6317";
        protected static string UserPoolSecret { get; set; } = "6671136fa932eb692735a6f82af3b67b";
        protected static string AppSecret { get; set; } = "e1497fad2404fcef1065c4b386cbc5fc";
        protected static string AppId { get; set; } = "62a99822ff635db21c2ec21c";

        protected static string TestUserId = "61a82941979c96c04ed9e920";

#if TEST_ENV
        public static string Host { get; set; } = "https://core.authing.cn";
#elif DEV_ENV
        public static string Host { get; set; } = "https://core.dev.authing-inc.co";
#else
        public static string Host { get; set; } = "https://core.authing.cn";
        //public static string Host { get; set; } = "https://newtest.authing.cn";
#endif
    }
}