﻿namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        //protected static string UserPoolId { get; set; } = "617280674680a6ca2b1f6317";
        //protected static string Secret { get; set; } = "6671136fa932eb692735a6f82af3b67b";
        //protected static string AppId { get; set; } = "6172807001258f603126a78a";

        //protected static string TestUserId = "61a82941979c96c04ed9e920";

        protected static string UserPoolId { get; set; } = "617280674680a6ca2b1f6317";
        protected static string Secret { get; set; } = "6671136fa932eb692735a6f82af3b67b";
        protected static string AppId { get; set; } = "61c2d04b36324259776af784";

        protected static string TestUserId = "61a82941979c96c04ed9e920";

#if TEST_ENV
        public static string Host { get; set; } = "https://core.authing.cn";
#elif DEV_ENV
        public static string Host { get; set; } = "https://core.dev.authing-inc.co";
#else
        public static string Host { get; set; } = "https://newtest.authing.cn";
#endif
    }
}