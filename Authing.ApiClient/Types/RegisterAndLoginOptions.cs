using System.Collections.Generic;

namespace Authing.ApiClient.Types
{
    public class RegisterAndLoginOptions
    {
        public bool ForceLogin { get; set; }
        public bool GenerateToken { get; set; }
        public bool AutoRegister { get; set; } = false;

        public string ClientIp { get; set; }
        public KeyValueDictionary[] CustomData { get; set; }

        public Dictionary<string, object>[] Context { get; set; }

        public string CaptchaCode { get; set; }
    }
}