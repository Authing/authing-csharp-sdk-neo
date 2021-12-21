using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class UserAction
    {
        [JsonProperty("operatorArn")]
        public string OperatorArn { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty("geoip")]
        public Geoip Geoip { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ua")]
        public Ua Ua { get; set; }

        [JsonProperty("userPoolId")]
        public string UserPoolId { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("operationName")]
        public string OperationName { get; set; }

        [JsonProperty("clientIp")]
        public string ClientIp { get; set; }

        [JsonProperty("extraData")]
        public string ExtraData { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("app")]
        public App App { get; set; }

        [JsonProperty("operationDesc")]
        public string OperationDesc { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("appName")]
        public string AppName { get; set; }
    }
}
