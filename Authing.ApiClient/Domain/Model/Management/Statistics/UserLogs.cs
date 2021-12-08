using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Statistics
{
    public class UserLogs
    {
        public int TotalCount { get; set; }

        public IEnumerable<UserLogsInfo> List { get; set; }
    }
}