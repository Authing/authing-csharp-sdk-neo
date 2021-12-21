using System;
namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class ListUserActionsParam
    {
        public string? ClientIp { get; set; }

        public string[]? OperationNames { get; set; }

        public string[]? UserIds { get; set; }

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;

        public string ExcludeNonAppRecords { get; set; }

        public string[]? AppIds { get; set; }

        public int? Start { get; set; }
        public int? End { get; set; }
    }
}
