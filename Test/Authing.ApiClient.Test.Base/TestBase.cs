﻿namespace Authing.ApiClient.Test.Base
{
    public abstract class TestBase
    {
        //protected static string UserPoolId { get; set; } = "6195ebce748aef66dc0b612d";
        //protected static string Secret { get; set; } = "4cb790e83a392f9ad8ee227e9e879839";
        //protected static string AppId { get; set; } = "6195ebcf5255f3d735ba9063";
        //protected static string TestUserId = "6195ffe645945c01d7ba9c94";

        //protected static string TestUserId = "61a82941979c96c04ed9e920";

        protected static string UserPoolId { get; set; } = "613189b2eed393affbbf396e";
        protected static string UserPoolSecret { get; set; } = "ccf4951a33e5d54d64e145782a65f0a7";
        protected static string AppSecret { get; set; } = "d453ef11f873527eb4a8a084f4b5e059";
        protected static string AppId { get; set; } = "62a9902a80f55c22346eb296";

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