using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Statistics;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IStatisticsManagement
    {
        Task<UserLogs> listUserActions(LogsPageParam options);
        Task<AdminLogs> listAuditLogs(AuditLogPageParam options);
    }
}