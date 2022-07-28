using System;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class UserActionOld
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


    public class UserAction
    {
        public string roleName { get; set; }
        public string timestamp { get; set; }
        public string eventResultMsg { get; set; }
        public string userId { get; set; }
        public string version { get; set; }
        public string roleCode { get; set; }
        public string appId { get; set; }
        public string requestId { get; set; }
        public string roleId { get; set; }
        public string targetValue { get; set; }
        public string operationParam { get; set; }
        public string userName { get; set; }
        public string userPoolId { get; set; }
        public string eventType { get; set; }
        public string targetId { get; set; }
        public Ua ua { get; set; }
        public string host { get; set; }
        public string originValue { get; set; }
        public string resourceDetails { get; set; }
        public string operationType { get; set; }
        public string eventDetails { get; set; }
        public string eventResultCode { get; set; }
        public string[] tags { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public string clientIp { get; set; }
        public Geoip geoip { get; set; }
        public string filedate { get; set; }
        public string userAgent { get; set; }
        public string operationMode { get; set; }
        public string logId { get; set; }
        public string appLogo { get; set; }
        public string photoUrl { get; set; }
        public string userPoolName { get; set; }
        public string appName { get; set; }
        public int loginCounts { get; set; }
    }
}
