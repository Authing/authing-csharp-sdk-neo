using System;
namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class UserActionRes
    {
        public string Id { get; set; }

        public string UserPoolId { get; set; }

        public string UserName { get; set; }

        public string CityName { get; set; }

        public string RegionName { get; set; }

        public string ClientIp { get; set; }

        public string OperationDesc { get; set; }

        public string OperationName { get; set; }

        public string TimeStamp { get; set; }

        public string AppId { get; set; }
        public string AppName { get; set; }
    }
}
