using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Statistics;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IStatisticsManagement
    {
        /// <summary>
        /// 用户操作日志
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        Task<UserLogs> listUserActions(LogsPageParam options);
        /// <summary>
        /// 审计日志
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<AdminLogs> listAuditLogs(AuditLogPageParam options);
    }
}