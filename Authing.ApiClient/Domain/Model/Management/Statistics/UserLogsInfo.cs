using System;

namespace Authing.ApiClient.Domain.Model.Management.Statistics
{
    public class UserLogsInfo
    {
        public string PhotoUrl { get; set; }
        public string EventType { get; set; }
        public string UserId { get; set; }
        public string LogId { get; set; }
        public string UserAgent { get; set; }
        public string ResourceName { get; set; }
        public string Message { get; set; }
        public string OperationParam { get; set; }
        public string EventResultMsg { get; set; }
        public string userPoolId { get; set; }
        public string RoleId { get; set; }
        public string RequestId { get; set; }
        public string OperationType { get; set; }
        public string TargetValue { get; set; }
        public string OperationMode { get; set; }
        public string EventResultCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string Host { get; set; }
        public string AppId { get; set; }
        public string Path { get; set; }
        public string ResourceDetails { get; set; }
        public string RoleCode { get; set; }
        public string UserName { get; set; }
        public string UserPoolName { get; set; }
        public string ResourceType { get; set; }
        public string Version { get; set; }
        public string OriginValue { get; set; }
        public string EventDetails { get; set; }
        public Geoip Geoip { get; set; }
        public string RoleName { get; set; }
        public string ClientIp { get; set; }
        public Ua Ua { get; set; }
        public string Filedate { get; set; }
    }
}